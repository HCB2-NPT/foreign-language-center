namespace ABC.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UpdateChanges_104
    {
        public void CreateStores()
        {
            #region a dbo.AddStudent
            CreateStoredProcedure("dbo.AddStudent",
                p => new
                {
                    p0 = p.String(),
                    p1 = p.String(),
                    p2 = p.DateTime(),
                    p3 = p.String()
                },
                body:
            @" BEGIN
                SET TRANSACTION ISOLATION LEVEL read committed  
                    begin transaction
                    if not exists (select PersonalID From Student where PersonalID = @p0)
                    begin
		                if @p2 <= GetDate()
		                begin
    		                begin try
    			                Insert into Student values (@p0,@p1,@p2,@p3)
    			                commit
    			                return
    		                end try
    		                begin catch
    			                rollback
				                return
    		                end catch
		                end
                    end
	                commit
	                return
                END"
            );
            #endregion
            #region b dbo.AddRegister
            CreateStoredProcedure("dbo.AddRegister",
                p => new
                {
                    p0 = p.String(),
                    p1 = p.String()
                },
                body:
                @"  BEGIN
                    begin transaction
                    SET TRANSACTION ISOLATION LEVEL read committed
	                if not exists (select * from Register where @p0=StudentId and @p1=TestScheduleId)
	                    begin
		                    begin try
			                    declare @datereg datetime
			                    set @datereg = GETDATE()
			                    Insert into Register(StudentId,TestScheduleId,DateReg) values (@p0,@p1,@datereg)
			                    commit transaction
                                return
		                    end try
		                    begin catch
			                    rollback
                                return
		                    end catch
	                    end
                    END"
            );
            #endregion
            #region c dbo.ListTested
            CreateStoredProcedure("dbo.ListTested",
                p => new
                {
                    p0 = p.String()
                },
                body:
                @"
                Set transaction isolation level read uncommitted
                begin transaction
	                if exists (select * from Student where PersonalId=@p0)
	                begin
				        select TestSchedule.TestScheduleId, TestSchedule.CertificateId, CONVERT(VARCHAR(10), TestSchedule.Date, 103) + ' '  + convert(VARCHAR(8), TestSchedule.Date, 14), Agency.Name, Certificate.Name, Certificate.Fee
				        from Agency,Register,TestSchedule,Certificate
				        where 
				        Agency.AgencyId=TestSchedule.AgencyId and
				        TestSchedule.CertificateId = Certificate.Name and
				        TestSchedule.TestScheduleId = Register.TestScheduleId and
				        TestSchedule.Date  <= GETDATE() and
                        Register.StudentID = @p0
	                end
                ");
            #endregion
            #region d dbo.RegisterTestOnline
            CreateStoredProcedure("dbo.RegisterTestOnline",
                p => new
                {
                    p0 = p.String(),
                    p1 = p.String()
                },
                body:
                @"
                Set transaction isolation level serializable
                begin transaction
	                begin 
		                declare @datereg datetime
                        select @datereg=Date from TestSchedule where TestSchedule.TestScheduleId = @p1
                        if exists(select Date
                                    from TestSchedule ,Register
                                    where DATEDIFF(HOUR,@datereg,Date) between -120 and 120
                                    and Register.TestScheduleId = TestSchedule.TestScheduleId
                                    and Register.StudentId = @p0
                                    )
		                begin
			                rollback
			                return
		                end
		                else
		                begin
			                declare @temp int 
			                select @temp=count(Register.StudentId)
			                from TestSchedule left join Register on TestSchedule.TestScheduleId = Register.TestScheduleId
			                where TestSchedule.TestScheduleId = @p1
			                group by TestSchedule.TestScheduleId
			
			                if @temp < 16
                            begin
			                    begin try
				                    Insert into Register(StudentId,TestScheduleId,DateReg) values (@p0,@p1,getdate())
				                    commit transaction
			                    end try
			                    begin catch
				                    rollback
				                    return
			                    end catch
                            end
                            else
                            begin
                                rollback
                                return
                            end
		                end
	                end
                ");
            #endregion
            #region e dbo.CheckListTestSchedule
            CreateStoredProcedure("dbo.CheckListTestSchedule",
                p => new
                {
                    p0 = p.String()
                },
                body://SELECT TestSchedule.TestScheduleId, TestSchedule.CertificateId, CONVERT(VARCHAR(10), TestSchedule.Date, 103) + ' '  + convert(VARCHAR(8), TestSchedule.Date, 14), Agency.Name, Certificate.Name, Certificate.Fee
                @"
                Set transaction isolation level read uncommitted
                begin transaction
                    if @p0 != ''
                    begin
	                    select TestSchedule.TestScheduleId, TestSchedule.CertificateId, CONVERT(VARCHAR(10), TestSchedule.Date, 103) + ' '  + convert(VARCHAR(8), TestSchedule.Date, 14), Agency.Name, Certificate.Name, Certificate.Fee
	                    from TestSchedule, Register, Agency, Certificate 
	                    where TestSchedule.Date >=getdate() and 
                        TestSchedule.TestScheduleId = Register.TestScheduleId and
                        TestSchedule.AgencyId = Agency.AgencyId and
                        TestSchedule.CertificateId = Certificate.Name and
                        Register.StudentId = @p0
	                    order by Date desc
                        commit transaction
                        return
                    end
                rollback
                ");
            #endregion
            #region f dbo.CheckTestScore
            CreateStoredProcedure("dbo.CheckTestScore",p=>new{
                p0 = p.String()
            },body:
            @"
            Set transaction isolation level read uncommitted
            begin transaction
	            if exists(select StudentId from Register where StudentId = @p0)
		        begin
			        select TestSchedule.TestScheduleId,TestSchedule.CertificateId, TestSchedule.Date, Agency.Name, Register.TestScore
			        from Register, TestSchedule, Agency
			        where Register.StudentId = @p0
				        AND getdate() > TestSchedule.Date
				        AND Register.TestScheduleId = TestSchedule.TestScheduleId
				        AND TestSchedule.AgencyId = Agency.AgencyId
			        commit transaction
			        return
		        end
		        else
		        begin
			        rollback
			        return
		        end"
            );
            #endregion
            #region g dbo.ReportNumberStudent_Certificate
            CreateStoredProcedure("dbo.ReportNumberStudent_Certificate", p => new
            {
                p0 = p.String()
            }, body:
            @"
                set transaction isolation level read committed
                begin transaction
                if exists (select * from Certificate where Name=@p0)
                begin
	                select Name,Count(StudentId) as Number
	                from Certificate,TestSchedule,Register
	                where Certificate.Name = TestSchedule.CertificateId and
	                TestSchedule.TestScheduleId = Register.TestScheduleId
	                and Certificate.Name = @p0
	                Group by Name
                end
                else
                begin
	                select Name,Count(StudentId) as Number
	                from Certificate,TestSchedule,Register
	                where Certificate.Name = TestSchedule.CertificateId and
	                TestSchedule.TestScheduleId = Register.TestScheduleId
	                Group by Name
                end
                commit transaction
            ");
            #endregion
            #region h dbo.ReportNumberStudent_Certificate_Date
            CreateStoredProcedure("dbo.ReportNumberStudent_Certificate_Date", p => new
            {
                p0 = p.String(),
                p1 = p.Int(),
                p2 = p.Int()
            }, body:
            @"
                set transaction isolation level read committed
                begin transaction
                if exists (select * from Certificate where Name=@p0)
                begin
	                select Name,Count(StudentId) as Number
	                from Certificate,TestSchedule,Register
	                where Certificate.Name = TestSchedule.CertificateId and
	                TestSchedule.TestScheduleId = Register.TestScheduleId and
	                Certificate.Name = @p0 and
	                DATEPART(year,Date) = @p2 and
	                DATEPART(month,Date) = @p1
	                Group by Name
                end
                else
                begin
	                select Name,Count(StudentId) as Number
	                from Certificate,TestSchedule,Register
	                where Certificate.Name = TestSchedule.CertificateId and
	                TestSchedule.TestScheduleId = Register.TestScheduleId and
	                DATEPART(year,Date) = @p2 and
	                DATEPART(month,Date) = @p1
	                Group by Name
                end
                commit transaction
            ");
            #endregion
            #region i dbo.ReportNumberStudent_Certificate_Date_Quarter
            CreateStoredProcedure("dbo.ReportNumberStudent_Certificate_Date_Quarter", p => new
            {
                p0 = p.String(),
                p1 = p.Int(),
                p2 = p.Int()
            }, body:
            @"
                set transaction isolation level read committed
                begin transaction
                if exists (select * from Certificate where Name=@p0)
                begin
		            select Name,Count(StudentId) as Number
		            from Certificate,TestSchedule,Register
		            where Certificate.Name = TestSchedule.CertificateId and
		            TestSchedule.TestScheduleId = Register.TestScheduleId and
		            Certificate.Name = @p0 and
		            DATEPART(year,Date) = @p2 and
		            DATEPART(month,Date) between 3*@p1-2 and 3*@p1
		            Group by Name
                end
                commit transaction
            ");
            #endregion
            #region j dbo.SumFee_Certificates
            CreateStoredProcedure("dbo.SumFee_Certificates", p => new
            {
                p0 = p.String()
            }, body:
            @"
                set transaction isolation level Serializable
                begin transaction
                if exists (select * from Certificate where Name=@p0)
                begin
	                select Certificate.Name,Sum(Fee) as TotalFee
	                from Certificate,TestSchedule,Agency,Register
	                where Certificate.Name=TestSchedule.CertificateId and 
                    TestSchedule.AgencyId=Agency.AgencyId and 
                    TestSchedule.TestScheduleId=Register.TestScheduleId and
	                Certificate.Name=@p0
	                group by Certificate.Name
                end
                else
                begin
	                select Certificate.Name,Sum(Fee) as TotalFee
	                from Certificate,TestSchedule,Agency,Register
	                where Certificate.Name=TestSchedule.CertificateId and 
                    TestSchedule.AgencyId=Agency.AgencyId and 
                    TestSchedule.TestScheduleId=Register.TestScheduleId
	                group by Certificate.Name
                end
                commit transaction
            ");
            #endregion
            #region k dbo.SumFee_Certificates_Date
            //k
            CreateStoredProcedure("dbo.SumFee_Certificates_Date", p => new
            {
                p0 = p.String(),
                p1 = p.Int(),
                p2 = p.Int()
            }, body:
            @"
                set transaction isolation level Serializable
                begin transaction
                if exists (select * from Certificate where Name=@p0)
                begin
	                select Certificate.Name,Sum(Fee) as TotalFee
	                from Certificate,TestSchedule,Agency,Register
	                where Certificate.Name=TestSchedule.CertificateId and 
	                TestSchedule.AgencyId=Agency.AgencyId and 
	                TestSchedule.TestScheduleId=Register.TestScheduleId and
	                Certificate.Name = @p0 and
	                DATEPART(year,Register.DateReg) = @p2 and
	                DATEPART(month,Register.DateReg) = @p1
	                group by Certificate.Name
                end
                else
                begin
	                select Certificate.Name,Sum(Fee) as TotalFee
	                from Certificate,TestSchedule,Agency,Register
	                where Certificate.Name=TestSchedule.CertificateId and 
	                TestSchedule.AgencyId=Agency.AgencyId and 
	                TestSchedule.TestScheduleId=Register.TestScheduleId and
	                DATEPART(year,Register.DateReg) = @p2 and
	                DATEPART(month,Register.DateReg) = @p1
	                group by Certificate.Name
                end
                commit transaction
            ");
            #endregion
            #region l dbo.SumFee_Certificates_Date_Quarter
            //l
            CreateStoredProcedure("dbo.SumFee_Certificates_Date_Quarter", p => new
            {
                p0 = p.String(),
                p1 = p.Int(),
                p2 = p.Int()
            }, body:
            @"
                set transaction isolation level Serializable
                begin transaction
                if exists (select * from Certificate where Name=@p0)
                begin
		            select Certificate.Name,Sum(Fee) as TotalFee
		            from Certificate,TestSchedule,Agency,Register
		            where Certificate.Name=TestSchedule.CertificateId and 
		            TestSchedule.AgencyId=Agency.AgencyId and 
		            TestSchedule.TestScheduleId=Register.TestScheduleId and
		            Certificate.Name = @p0 and
		            DATEPART(year,Register.DateReg) = @p2 and
		            DATEPART(month,Register.DateReg) between 3*@p1-2 and 3*@p1
		            group by Certificate.Name
	            end
                commit transaction
            ");
            #endregion
            #region m dbo.ResultTest_Certificate
            //m
            CreateStoredProcedure("dbo.ResultTest_Certificate", p => new
            {
            }, body:
            @"
                set transaction isolation level read uncommitted
                begin transaction
	                select Register.StudentId,Certificate.Name,Agency.Name,TestSchedule.Date,TestScore 
	                from Certificate,TestSchedule,Agency,Register
	                where
	                Certificate.Name = TestSchedule.CertificateId and
	                TestSchedule.AgencyId = Agency.AgencyId and
	                TestSchedule.TestScheduleId = Register.TestScheduleId and
	                TestSchedule.Date <= GetDate()
	                order by TestScore asc
                commit transaction
            ");
            #endregion
            #region n dbo.ResultTest_Certificate_Date
            CreateStoredProcedure("dbo.ResultTest_Certificate_Date", p => new
            {
                p0 = p.String(),
                p1 = p.Int(),
                p2 = p.Int()
            }, body:
            @"
                set transaction isolation level read uncommitted
                begin transaction
	                select Register.StudentId,Certificate.Name,Agency.Name,TestSchedule.Date,TestScore 
	                from Certificate,TestSchedule,Agency,Register
	                where Certificate.Name = @p0 and
	                Certificate.Name = TestSchedule.CertificateId and
	                TestSchedule.AgencyId = Agency.AgencyId and
	                TestSchedule.TestScheduleId = Register.TestScheduleId and
	                TestSchedule.Date <= GetDate() and
	                DATEPART(year,TestSchedule.Date) = @p2 and
	                DATEPART(month,TestSchedule.Date) = @p1
	                order by TestScore asc
                commit transaction
            ");
            #endregion
            #region o dbo.ResultTest_Certificate_Date_Quarter
            CreateStoredProcedure("dbo.ResultTest_Certificate_Date_Quarter", p => new
            {
                p0 = p.String(),
                p1 = p.Int(),
                p2 = p.Int()
            }, body:
            @"
                set transaction isolation level read uncommitted
                begin transaction
	                begin
		                select Register.StudentId,Certificate.Name,Agency.Name,TestSchedule.Date,TestScore 
		                from Certificate,TestSchedule,Agency,Register
		                where Certificate.Name = @p0 and
		                Certificate.Name = TestSchedule.CertificateId and
		                TestSchedule.AgencyId = Agency.AgencyId and
		                TestSchedule.TestScheduleId = Register.TestScheduleId and
		                TestSchedule.Date <= GetDate() and
		                DATEPART(year,TestSchedule.Date) = @p2 and
		                DATEPART(month,TestSchedule.Date) between 3*@p1-2 and 3*@p1
		                order by TestScore asc
	                end
                commit transaction
            ");
            #endregion

            #region Helper dbo.getTestSchedules
            CreateStoredProcedure("dbo.usp_getTestSchedules",
            body:
            @"
                SET TRANSACTION ISOLATION LEVEL READ COMMITTED
                BEGIN
                    SELECT TestSchedule.TestScheduleId, TestSchedule.CertificateId, CONVERT(VARCHAR(10), TestSchedule.Date, 103) + ' '  + convert(VARCHAR(8), TestSchedule.Date, 14), Agency.Name, Certificate.Name, Certificate.Fee
                    FROM TestSchedule, Agency, Certificate 
                    WHERE TestSchedule.Date >= GETDATE()
                        AND TestSchedule.AgencyId = Agency.AgencyId
                        AND TestSchedule.CertificateId = Certificate.Name
                        AND TestSchedule.Date <= DATEADD(DAY, 90, GETDATE())
                    ORDER BY TestSchedule.Date DESC
                END
            ");
            #endregion
        }

        public void DropStores()
        {

        }
    }
}
