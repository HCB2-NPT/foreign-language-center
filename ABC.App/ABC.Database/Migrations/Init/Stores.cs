namespace ABC.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate
    {
        public void CreateStores()
        {
            CreateStoredProcedure("dbo.AddStudent",
                p => new
                {
                    PersonalID = p.String(),
                    FullName = p.String(),
                    BirthDay = p.DateTime(),
                    PhoneNumber = p.String()
                },
                body:
            @"begin transaction
                if not exists (select PersonalID From Student where PersonalID = @PersonalID)
                begin
                    if @BirthDay <= GetDate()
                    begin
    				    begin try
    					    Insert into Student values (@PersonalID,@FullName,@BirthDay,@PhoneNumber)
    				    end try
    				    begin catch
    					    rollback
                            return
    				    end catch
                    end
                end
                commit"
            );
        }

        public void DropStores()
        {

        }
    }
}
