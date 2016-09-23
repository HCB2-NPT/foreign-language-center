namespace ABC.Database.Migrations
{
    using ABC.Database.Objects;
    using ABC.Database.ObjectContexts;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyDatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MyDatabaseContext context)
        {
            this.seedingAgency(context);
            this.seedingCertificate(context);
            this.seedingStudent(context);
            this.seedingTestSchedules(context);
        }

        private void seedingAgency(MyDatabaseContext context)
        {
            string[] agencies = {
                "National University of Singapore",
                "Nanyang Technological University",
                "Singapore Management University",
                "Singapore University of Technology and Design",
                "Singapore Institute of Technology",
                "SIM University"
            };

            for (int i = 0; i < agencies.Length; i++)
            {
                context.Agencies.AddOrUpdate(
                    a => a.Id,
                    new Agency { 
                        Id = i + 1, 
                        Name = agencies[i] 
                    }
                );
            }
        }

        private void seedingCertificate(MyDatabaseContext context)
        {
            Random rand = new Random();
            string[] certificates = {
                "TOEIC",
                "TOEFL",
                "TFI",
                "JPT",
                "EL"
            };
            
            for (int i = 0; i < certificates.Length; i++)
            {
                context.Certificates.AddOrUpdate(
                    c => c.Name,
                    new Certificate {
                        Name = certificates[i], 
                        Fee = rand.Next(50, 150) * 10000
                    }
                );
            }
        }

        private void seedingStudent(MyDatabaseContext context)
        {
            Random rand = new Random();

            string[] fistNames = {
                "Azalee", "Marchelle", "Jacob", "Gregoria", "Crystal", "Keitha", "Armandina", "Renetta", "Emerson", "Angelika",
                "Pablo", "Ewa", "Basil", "Dana", "Randi", "Raymon", "Anna", "April", "Malinda", "Clarissa",
                "Giovanni", "Grayce", "Tennille", "Ricky", "Lamonica", "Victoria", "Tam", "Peggie", "Bernardo", "Chase",
                "Nona", "Ellis", "Kiley", "Caridad", "Trina", "Lizzette", "Juan", "Bob", "Andera", "Cherilyn"
            };

            string[] lastNames = {
                "Bickham", "Toman", "Scardina", "Coonrod", "Keesee", "Concepcion", "Peaden", "Kwong", "Raymo", "Mccallister",
                "Barlett", "Blane", "Dumond", "Noonan", "Hinds", "Skalski", "Sangster", "Crepeau", "Milewski", "Dangelo",
                "Sabatini", "Slayden", "Ralston", "Tate", "Earlywine", "Breault", "Romriell", "Phoenix", "Avans", "Bridgett",
                "Fermin", "Yonts", "Greenhalgh", "Schwein", "Sublett", "Cromwell", "Middaugh", "Mullenix", "Bent", "Bermudez"
            };

            for (int index = 0; index < 200; index++)
            {
                context.Students.AddOrUpdate(
                    s => s.PersonalId,
                    new Student
                    {
                        PersonalId = "0250000" + index.ToString("000"),
                        FullName = String.Format("{0} {1}", fistNames[rand.Next(fistNames.Length)], lastNames[rand.Next(lastNames.Length)]),
                        Birthday = new DateTime(rand.Next(1980, 2000), rand.Next(1, 12), rand.Next(1, 29)),
                        PhoneNumber = "09" + rand.Next(10000000, 99999999).ToString()
                    }
                );
            }
        }

        private void seedingTestSchedules(MyDatabaseContext context)
        {
            Random rand = new Random();
            int[] times = { 8, 14 };
            int day, month, numberOfStudents;

            string[] certificates = {
                "TOEIC",
                "TOEFL",
                "TFI",
                "JPT",
                "EL"
            };

            for (int index = 0; index < 100; index++) {
                month = rand.Next(3, 9);
                day = rand.Next(7, 28);
                numberOfStudents = rand.Next(1, 16);

                context.TestSchedules.AddOrUpdate(
                    t => t.Id,
                    new TestSchedule
                    {
                        Id = index + 1,
                        Date = new DateTime(2016, month, day, times[rand.Next(1)], 0, 0),
                        AgencyId = rand.Next(1, 6),
                        CertificateId = certificates[rand.Next(certificates.Length)]
                    }
                );

                for (int sub_index = 0; sub_index < numberOfStudents; sub_index++)
                {
                    context.Registers.AddOrUpdate(
                        r => r.RegisterID,
                        new Register
                        {
                            RegisterID = String.Format("{0}{1}", (index + 1).ToString("00"), (sub_index + 1).ToString("00")),
                            StudentId = "0250000" + rand.Next(1, 200).ToString("000"),
                            TestScheduleId = index + 1,
                            DateReg = new DateTime(2016, month, day - rand.Next(3, 4), rand.Next(8, 17), rand.Next(59), rand.Next(59)),
                            TestScore = rand.Next(30, 100)
                        }
                    );
                }
            }
        }
    }
}
