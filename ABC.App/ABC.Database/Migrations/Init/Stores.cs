namespace ABC.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UpdateChanges_104
    {
        public void CreateStores()
        {
            //a
            CreateStoredProcedure("dbo.AddStudent",
                p => new
                {
                    PersonalID = p.String(),
                    FullName = p.String(),
                    BirthDay = p.DateTime(),
                    PhoneNumber = p.String()
                },
                body:
            @" BEGIN
	                SET TRANSACTION ISOLATION LEVEL read committed  
                    begin transaction
                    if not exists (select PersonalID From Student where PersonalID = @PersonalID)
                    begin
                    if @BirthDay <= GetDate()
                    begin
		                begin try
			                Insert into Student values (@PersonalID,@FullName,@BirthDay,@PhoneNumber)
			                commit transaction
			                return
		                end try
		                begin catch
			                rollback
			                return
		                end catch
                    end
                    end
                END"
            );

            //b
            CreateStoredProcedure("dbo.AddRegister",
                p => new
                {
                    studentid = p.String(),
                    testschedule = p.String()
                },
                body:
                @"  BEGIN
                    begin transaction
                    SET TRANSACTION ISOLATION LEVEL read committed
	                if not exists (select * from Register where @studentid=StudentId and @testschedule=TestScheduleId)
	                    begin
		                    begin try
			                    declare @datereg datetime
			                    set @datereg = GETDATE()
			                    Insert into Register(StudentId,TestScheduleId,DateReg) values (@studentid,@testschedule,@datereg)
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
            //c
            CreateStoredProcedure("dbo.CheckListTestSchedule",
                p => new
                {
                    studentid = p.String()
                },
                body:
                @"
                Set transaction isolation level read uncommitted
                begin transaction
	                select [TestSchedule].[CertificateId] as c,[TestSchedule].[Date] as d,[Register].[TestScore] as t
	                from [dbo].[TestSchedule] join [dbo].[Register] on [dbo].[TestSchedule].[TestScheduleId] = [dbo].[Register].[TestScheduleId]
	                where ([TestSchedule].[Date]<=getdate() and 
                    [Register].[StudentId] = @studentid )
	                order by [Date] desc
                commit transaction
                ");
            
            //d

            CreateStoredProcedure("dbo.RegisterTestOnline",
                p => new
                {
                    studentid = p.String(),
                    testid = p.String()
                },
                body:
                @"
                Set transaction isolation level serializable
                begin transaction
	                begin 
		                declare @datereg datetime
		                select @datereg=Date from TestSchedule where TestSchedule.TestScheduleId = @testid
		                declare @dt datetime
		                select @dt=Max(TestSchedule.Date)
		                from TestSchedule,Register
		                where TestSchedule.TestScheduleId = Register.TestScheduleId and StudentId = @studentid

		                if ((DATEDIFF(HOUR,@dt,@datereg)/24) < 5 and (DATEDIFF(HOUR,@dt,@datereg)/24) > -5) or Getdate()>@datereg
		                begin
			                rollback
			                return
		                end
		                else
		                begin
			                declare @temp int 
			                select @temp=count(Register.StudentId)
			                from TestSchedule left join Register on TestSchedule.TestScheduleId = Register.TestScheduleId
			                where TestSchedule.TestScheduleId = @testid
			                group by TestSchedule.TestScheduleId
			
			                if @temp < 16
			                begin try
				                Insert into Register(StudentId,TestScheduleId,DateReg) values (@studentid,@testid,@datereg)
				                commit transaction
			                end try
			                begin catch
				                rollback
				                return
			                end catch
		                end
	                end
                ");

            //e
            CreateStoredProcedure("dbo.ListTested",
                p => new
                {
                    certificateid = p.String()
                },
                body:
                @"
                Set transaction isolation level read uncommitted
                begin transaction
	                if exists (select Name from [Certificate] where Name=@certificateid)
	                begin
		                begin transaction
				                select TestSchedule.TestScheduleId,TestSchedule.Date,Agency.Name,[Certificate].Name,Fee,COUNT(Register.StudentID) as SoLuongDaDangKy
				                from Agency,Register,TestSchedule,[Certificate]
				                where 
				                Agency.AgencyId=TestSchedule.AgencyId and
				                TestSchedule.CertificateId = [Certificate].Name and
				                TestSchedule.TestScheduleId = Register.TestScheduleId and
				                [Certificate].Name=@certificateid and
				                TestSchedule.Date > = GETDATE()
				                group by TestSchedule.TestScheduleId,TestSchedule.Date,Agency.Name,[Certificate].Name,Fee 
		                commit transaction
	                end
	                else
	                begin
		                begin transaction
		                select TestSchedule.TestScheduleId,TestSchedule.Date,Agency.Name,[Certificate].Name,Fee,COUNT(Register.StudentID) as SoLuongDaDangKy
				                from Agency,Register,TestSchedule,[Certificate]
				                where 
				                Agency.AgencyId=TestSchedule.AgencyId and
				                TestSchedule.CertificateId = [Certificate].Name and
				                TestSchedule.TestScheduleId = Register.TestScheduleId and
				                TestSchedule.Date > = GETDATE()
				                group by TestSchedule.TestScheduleId,TestSchedule.Date,Agency.Name,[Certificate].Name,Fee 
		                commit transaction
	                end
                ");

            //f
            CreateStoredProcedure("dbo.CheckTestScore",p=>new{
                studentid = p.String()
            },body:
            @"
            Set transaction isolation level read uncommitted
            begin transaction
	            if exists(select * from Student where PersonalId = @studentid)
		            begin 
			            if exists(select * from Register where StudentId = @studentid)
			            begin
				            select TestSchedule.TestScheduleId,TestSchedule.Date,Agency.Name,Register.TestScore
				            from Register,TestSchedule,Agency
				            where Register.StudentId=@studentid and
				            Register.TestScheduleId=TestSchedule.TestScheduleId and
				            TestSchedule.AgencyId = Agency.AgencyId
				            commit transaction
				            return
			            end
			            else
			            begin
				            rollback
				            return
			            end
		            end
	            else
	            begin
		            rollback
		            return
	            end"
            );

            //g
            CreateStoredProcedure("dbo.ReportNumberStudent_Certificate", p => new
            {
                certificate = p.String()
            }, body:
            @"
                set transaction isolation level read committed
                begin transaction
                if exists (select * from Certificate where Name=@certificate)
                begin
	                select Name,Count(StudentId) as SoLuong
	                from Certificate,TestSchedule,Register
	                where Certificate.Name = TestSchedule.CertificateId and
	                TestSchedule.TestScheduleId = Register.TestScheduleId
	                and Certificate.Name = @certificate and
	                TestSchedule.Date >= GETDATE()
	                Group by Name
                end
                else
                begin
	                select Name,Count(StudentId) as SoLuong
	                from Certificate,TestSchedule,Register
	                where Certificate.Name = TestSchedule.CertificateId and
	                TestSchedule.TestScheduleId = Register.TestScheduleId and
	                TestSchedule.Date >= GETDATE()
	                Group by Name
                end
                commit transaction
            ");

            //h
            CreateStoredProcedure("dbo.ReportNumberStudent_Certificate_Date", p => new
            {
                certificate = p.String(),
                month = p.Int(),
                year = p.Int()
            }, body:
            @"
                set transaction isolation level read committed
                begin transaction
                if exists (select * from Certificate where Name=@certificate)
                begin
	                select Name,Count(StudentId) as SoLuong
	                from Certificate,TestSchedule,Register
	                where Certificate.Name = TestSchedule.CertificateId and
	                TestSchedule.TestScheduleId = Register.TestScheduleId and
	                Certificate.Name = @certificate and
	                DATEPART(year,Date) = @year and
	                DATEPART(month,Date) = @month
	                Group by Name
                end
                else
                begin
	                select Name,Count(StudentId) as SoLuong
	                from Certificate,TestSchedule,Register
	                where Certificate.Name = TestSchedule.CertificateId and
	                TestSchedule.TestScheduleId = Register.TestScheduleId and
	                DATEPART(year,Date) = @year and
	                DATEPART(month,Date) = @month
	                Group by Name
                end
                commit transaction
            ");

            //i
            CreateStoredProcedure("dbo.ReportNumberStudent_Certificate_Date_Quy", p => new
            {
                certificate = p.String(),
                quy = p.Int(),
                year = p.Int()
            }, body:
            @"
                set transaction isolation level read committed
                begin transaction
                if exists (select * from Certificate where Name=@certificate)
                begin
		            select Name,Count(StudentId) as SoLuong
		            from Certificate,TestSchedule,Register
		            where Certificate.Name = TestSchedule.CertificateId and
		            TestSchedule.TestScheduleId = Register.TestScheduleId and
		            Certificate.Name = @certificate and
		            DATEPART(year,Date) = @year and
		            DATEPART(month,Date) between 3*@quy-2 and 3*@quy
		            Group by Name
                end
                commit transaction
            ");

            //j
            CreateStoredProcedure("dbo.SumFee_Certificates", p => new
            {
                certificate = p.String(),
            }, body:
            @"
                set transaction isolation level Serializable
                begin transaction
                if exists (select * from Certificate where Name=@certificate)
                begin
	                select Certificate.Name,Sum(Fee) as DoanhThu
	                from Certificate,TestSchedule,Agency,Register
	                where Certificate.Name=TestSchedule.CertificateId and TestSchedule.AgencyId=Agency.AgencyId and TestSchedule.TestScheduleId=Register.TestScheduleId
	                and Certificate.Name=@certificate
	                group by Certificate.Name
                end
                else
                begin
	                select Certificate.Name,Sum(Fee) as DoanhThu
	                from Certificate,TestSchedule,Agency,Register
	                where Certificate.Name=TestSchedule.CertificateId and TestSchedule.AgencyId=Agency.AgencyId and TestSchedule.TestScheduleId=Register.TestScheduleId
	                group by Certificate.Name
                end
                commit transaction
            ");

            //k
            CreateStoredProcedure("dbo.SumFee_Certificates_Date", p => new
            {
                certificate = p.String(),
                month = p.Int(),
                year = p.Int()
            }, body:
            @"
                set transaction isolation level Serializable
                begin transaction
                if exists (select * from Certificate where Name=@certificate)
                begin
	                select Certificate.Name,Sum(Fee) as DoanhThu
	                from Certificate,TestSchedule,Agency,Register
	                where Certificate.Name=TestSchedule.CertificateId and 
	                TestSchedule.AgencyId=Agency.AgencyId and 
	                TestSchedule.TestScheduleId=Register.TestScheduleId and
	                Certificate.Name = @certificate and
	                DATEPART(year,Register.DateReg) = @year and
	                DATEPART(month,Register.DateReg) = @month
	                group by Certificate.Name
                end
                else
                begin
	                select Certificate.Name,Sum(Fee) as DoanhThu
	                from Certificate,TestSchedule,Agency,Register
	                where Certificate.Name=TestSchedule.CertificateId and 
	                TestSchedule.AgencyId=Agency.AgencyId and 
	                TestSchedule.TestScheduleId=Register.TestScheduleId and
	                DATEPART(year,Register.DateReg) = @year and
	                DATEPART(month,Register.DateReg) = @month
	                group by Certificate.Name
                end
                commit transaction
            ");

            //l
            CreateStoredProcedure("dbo.SumFee_Certificates_Date_Quy", p => new
            {
                certificate = p.String(),
                quy = p.Int(),
                year = p.Int()
            }, body:
            @"
                set transaction isolation level Serializable
                begin transaction
                if exists (select * from Certificate where Name=@certificate)
                begin
		            select Certificate.Name,Sum(Fee) as DoanhThu
		            from Certificate,TestSchedule,Agency,Register
		            where Certificate.Name=TestSchedule.CertificateId and 
		            TestSchedule.AgencyId=Agency.AgencyId and 
		            TestSchedule.TestScheduleId=Register.TestScheduleId and
		            Certificate.Name = @certificate and
		            DATEPART(year,Register.DateReg) = @year and
		            DATEPART(month,Register.DateReg) between 3*@quy-2 and 3*@quy
		            group by Certificate.Name
	            end
                commit transaction
            ");

            //m
            CreateStoredProcedure("dbo.ResultTest_Certificate", p => new
            {
                studentid = p.String(),
                certificate = p.String()
            }, body:
            @"
                set transaction isolation level read uncommitted
                begin transaction
	                select Certificate.Name,Agency.Name,TestSchedule.Date,TestScore 
	                from Certificate,TestSchedule,Agency,Register
	                where Certificate.Name = @certificate and
	                Certificate.Name = TestSchedule.CertificateId and
	                TestSchedule.AgencyId = Agency.AgencyId and
	                TestSchedule.TestScheduleId = Register.TestScheduleId and
	                Register.StudentId = @studentid and
	                TestSchedule.Date <= GetDate()
	                order by  Certificate.Name,TestSchedule.Date asc
                commit transaction
            ");

            //n
            CreateStoredProcedure("dbo.ResultTest_Certificate_Date", p => new
            {
                studentid = p.String(),
                certificate = p.String(),
                month = p.Int(),
                year = p.Int()
            }, body:
            @"
                set transaction isolation level read uncommitted
                begin transaction
	                select Certificate.Name,Agency.Name,TestSchedule.Date,TestScore 
	                from Certificate,TestSchedule,Agency,Register
	                where Certificate.Name = @certificate and
	                Certificate.Name = TestSchedule.CertificateId and
	                TestSchedule.AgencyId = Agency.AgencyId and
	                TestSchedule.TestScheduleId = Register.TestScheduleId and
	                Register.StudentId = @studentid and
	                TestSchedule.Date <= GetDate() and
	                DATEPART(year,TestSchedule.Date) = @year and
	                DATEPART(month,TestSchedule.Date) = @month
	                order by  Certificate.Name,TestSchedule.Date asc
                commit transaction
            ");

            //o
            CreateStoredProcedure("dbo.ResultTest_Certificate_Date_Quy", p => new
            {
                studentid = p.String(),
                certificate = p.String(),
                quy = p.Int(),
                year = p.Int()
            }, body:
            @"
                set transaction isolation level read uncommitted
                begin transaction
	                begin
		                select Certificate.Name,Agency.Name,TestSchedule.Date,TestScore 
		                from Certificate,TestSchedule,Agency,Register
		                where Certificate.Name = @certificate and
		                Certificate.Name = TestSchedule.CertificateId and
		                TestSchedule.AgencyId = Agency.AgencyId and
		                TestSchedule.TestScheduleId = Register.TestScheduleId and
		                Register.StudentId = @studentid and
		                TestSchedule.Date <= GetDate() and
		                DATEPART(year,TestSchedule.Date) = @year and
		                DATEPART(month,TestSchedule.Date) between 3*@quy-2 and 3*@quy
		                order by  Certificate.Name,TestSchedule.Date asc
	                end
                commit transaction
            ");
             
        }

        public void DropStores()
        {

        }
    }
}
