using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Lebetak.Models
{
    public static class DataSeeder
    {
        public static void SeedRole(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "Client",
                    NormalizedName = "CLIENT"
                },
                new IdentityRole
                {
                    Id = "3",
                    Name = "Worker",
                    NormalizedName = "WORKER"
                },
                new IdentityRole
                {
                    Id = "4",
                    Name = "Owner",
                    NormalizedName = "OWNER"
                }
            );
        }

        public static void SeedPostStatus(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostStatus>().HasData(
                new PostStatus { Id = 1, Title = "مفتوح", Description = "الإعلان مفتوح لتلقي العروض" },
                new PostStatus { Id = 2, Title = "قيد التنفيذ", Description = "العمل قيد التنفيذ" },
                new PostStatus { Id = 3, Title = "مكتمل", Description = "تم إكمال العمل بنجاح" }

            );
        }
        public static void SeedUrgency(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Urgency>().HasData(
                new Urgency { Id=1,Title = "مستعجل",Description="احتاج تنفيذ هذة الخدمة في اسرع وقت"},
                new Urgency { Id=2,Title = "غير مستعجل",Description="يمكن تنفيذ الخدمة في وقت لاحق" },
                new Urgency { Id=3,Title = "عادي",Description="يمكن تنفيذ الخدمة خلال اليوم" },
                new Urgency { Id=4,Title = "هام",Description="يجب تنفيذ الخدمة خلال 24 ساعة" }
            );
        }

        // Categories
        public static void SeedCategorey(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "سباكة" },
            new Category { Id = 2, Name = "كهرباء" },
            new Category { Id = 3, Name = "نجارة" },
            new Category { Id = 4, Name = "دهان" },
            new Category { Id = 5, Name = "تكييف" },
            new Category { Id = 6, Name = "صيانة أجهزة منزلية" },
            new Category { Id = 7, Name = "تنظيف" },
            new Category { Id = 8, Name = "تصليح أثاث" },
            new Category { Id = 9, Name = "تركيب سيراميك وارضيات" },
            new Category { Id = 10, Name = "دهانات خارجية" },
            new Category { Id = 11, Name = "أعمال حدادة" },
            new Category { Id = 12, Name = "أعمال عزل" },
            new Category { Id = 13, Name = "أعمال الحدائق" },
            new Category { Id = 14, Name = "دهان داخلي" },
            new Category { Id = 15, Name = "خدمات طوارئ" }
            );
        }

        public static void SeedQuestions(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>().HasData(

                // 1 سباكة
                new Question { Id = 1, Text = "نوع المشكلة؟", CategoryId = 1 },
                new Question { Id = 2, Text = "مكان المشكلة؟", CategoryId = 1 },

                // 2 كهرباء
                new Question { Id = 3, Text = "نوع العطل الكهربائي؟", CategoryId = 2 },
                new Question { Id = 4, Text = "هل يوجد انقطاع كامل؟", CategoryId = 2 },

                // 3 نجارة
                new Question { Id = 5, Text = "نوع العمل المطلوب؟", CategoryId = 3 },
                new Question { Id = 6, Text = "نوع الخشب؟", CategoryId = 3 },

                // 4 دهان
                new Question { Id = 7, Text = "نوع الدهان؟", CategoryId = 4 },
                new Question { Id = 8, Text = "مساحة المكان؟", CategoryId = 4 },

                // 5 تكييف
                new Question { Id = 9, Text = "نوع جهاز التكييف؟", CategoryId = 5 },
                new Question { Id = 10, Text = "نوع المشكلة؟", CategoryId = 5 },

                // 6 صيانة أجهزة
                new Question { Id = 11, Text = "نوع الجهاز؟", CategoryId = 6 },
                new Question { Id = 12, Text = "نوع العطل؟", CategoryId = 6 },

                // 7 تنظيف
                new Question { Id = 13, Text = "نوع التنظيف؟", CategoryId = 7 },
                new Question { Id = 14, Text = "حجم المكان؟", CategoryId = 7 },

                // 8 تصليح أثاث
                new Question { Id = 15, Text = "نوع الأثاث؟", CategoryId = 8 },
                new Question { Id = 16, Text = "نوع الضرر؟", CategoryId = 8 },

                // 9 سيراميك وأرضيات
                new Question { Id = 17, Text = "نوع الأرضية؟", CategoryId = 9 },
                new Question { Id = 18, Text = "مساحة العمل؟", CategoryId = 9 },

                // 10 دهانات خارجية
                new Question { Id = 19, Text = "نوع المبنى؟", CategoryId = 10 },
                new Question { Id = 20, Text = "حالة الجدران؟", CategoryId = 10 },

                // 11 حدادة
                new Question { Id = 21, Text = "نوع العمل؟", CategoryId = 11 },
                new Question { Id = 22, Text = "مكان التنفيذ؟", CategoryId = 11 },

                // 12 عزل
                new Question { Id = 23, Text = "نوع العزل؟", CategoryId = 12 },
                new Question { Id = 24, Text = "مكان العزل؟", CategoryId = 12 },

                // 13 حدائق
                new Question { Id = 25, Text = "نوع الخدمة؟", CategoryId = 13 },
                new Question { Id = 26, Text = "مساحة الحديقة؟", CategoryId = 13 },

                // 14 دهان داخلي
                new Question { Id = 27, Text = "نوع الغرفة؟", CategoryId = 14 },
                new Question { Id = 28, Text = "نوع الدهان؟", CategoryId = 14 },

                // 15 طوارئ
                new Question { Id = 29, Text = "نوع الطوارئ؟", CategoryId = 15 },
                new Question { Id = 30, Text = "درجة الخطورة؟", CategoryId = 15 }
            );
        }


        public static void SeedOptions(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Option>().HasData(

                // Q1
                new Option { Id = 1, Text = "تسريب", QuestionId = 1 },
                new Option { Id = 2, Text = "انسداد", QuestionId = 1 },
                new Option { Id = 3, Text = "كسر", QuestionId = 1 },

                // Q2
                new Option { Id = 4, Text = "مطبخ", QuestionId = 2 },
                new Option { Id = 5, Text = "حمام", QuestionId = 2 },
                new Option { Id = 6, Text = "سطح", QuestionId = 2 },

                // Q3
                new Option { Id = 7, Text = "فصل كهرباء", QuestionId = 3 },
                new Option { Id = 8, Text = "تماس", QuestionId = 3 },
                new Option { Id = 9, Text = "عطل فيوز", QuestionId = 3 },

                // Q4
                new Option { Id = 10, Text = "نعم", QuestionId = 4 },
                new Option { Id = 11, Text = "لا", QuestionId = 4 },

                // Q5
                new Option { Id = 12, Text = "تفصيل", QuestionId = 5 },
                new Option { Id = 13, Text = "تصليح", QuestionId = 5 },
                new Option { Id = 14, Text = "تركيب", QuestionId = 5 },

                // Q6
                new Option { Id = 15, Text = "خشب طبيعي", QuestionId = 6 },
                new Option { Id = 16, Text = "MDF", QuestionId = 6 },
                new Option { Id = 17, Text = "كونتر", QuestionId = 6 },

                // Q7
                new Option { Id = 18, Text = "بلاستيك", QuestionId = 7 },
                new Option { Id = 19, Text = "زيت", QuestionId = 7 },
                new Option { Id = 20, Text = "لاكيه", QuestionId = 7 },

                // Q8
                new Option { Id = 21, Text = "صغيرة", QuestionId = 8 },
                new Option { Id = 22, Text = "متوسطة", QuestionId = 8 },
                new Option { Id = 23, Text = "كبيرة", QuestionId = 8 },

                // Q9
                new Option { Id = 24, Text = "سبليت", QuestionId = 9 },
                new Option { Id = 25, Text = "شباك", QuestionId = 9 },
                new Option { Id = 26, Text = "مركزي", QuestionId = 9 },

                // Q10
                new Option { Id = 27, Text = "لا يبرد", QuestionId = 10 },
                new Option { Id = 28, Text = "تسريب", QuestionId = 10 },
                new Option { Id = 29, Text = "صوت عالي", QuestionId = 10 },

                // Q11
                new Option { Id = 30, Text = "غسالة", QuestionId = 11 },
                new Option { Id = 31, Text = "ثلاجة", QuestionId = 11 },
                new Option { Id = 32, Text = "بوتاجاز", QuestionId = 11 },

                // Q12
                new Option { Id = 33, Text = "لا يعمل", QuestionId = 12 },
                new Option { Id = 34, Text = "ضعف أداء", QuestionId = 12 },
                new Option { Id = 35, Text = "صوت غريب", QuestionId = 12 },

                // Q13
                new Option { Id = 36, Text = "منزل", QuestionId = 13 },
                new Option { Id = 37, Text = "مكتب", QuestionId = 13 },
                new Option { Id = 38, Text = "شركة", QuestionId = 13 },

                // Q14
                new Option { Id = 39, Text = "صغير", QuestionId = 14 },
                new Option { Id = 40, Text = "متوسط", QuestionId = 14 },
                new Option { Id = 41, Text = "كبير", QuestionId = 14 },

                // Q15
                new Option { Id = 42, Text = "كرسي", QuestionId = 15 },
                new Option { Id = 43, Text = "دولاب", QuestionId = 15 },
                new Option { Id = 44, Text = "سرير", QuestionId = 15 },

                // Q16
                new Option { Id = 45, Text = "كسر", QuestionId = 16 },
                new Option { Id = 46, Text = "فك", QuestionId = 16 },
                new Option { Id = 47, Text = "تلف", QuestionId = 16 },

                // Q17
                new Option { Id = 48, Text = "سيراميك", QuestionId = 17 },
                new Option { Id = 49, Text = "رخام", QuestionId = 17 },
                new Option { Id = 50, Text = "باركيه", QuestionId = 17 },

                // Q18
                new Option { Id = 51, Text = "صغيرة", QuestionId = 18 },
                new Option { Id = 52, Text = "متوسطة", QuestionId = 18 },
                new Option { Id = 53, Text = "كبيرة", QuestionId = 18 },

                // Q19
                new Option { Id = 54, Text = "منزل", QuestionId = 19 },
                new Option { Id = 55, Text = "فيلا", QuestionId = 19 },
                new Option { Id = 56, Text = "عمارة", QuestionId = 19 },

                // Q20
                new Option { Id = 57, Text = "جيدة", QuestionId = 20 },
                new Option { Id = 58, Text = "متشققة", QuestionId = 20 },
                new Option { Id = 59, Text = "تحتاج ترميم", QuestionId = 20 },

                // Q21
                new Option { Id = 60, Text = "أبواب", QuestionId = 21 },
                new Option { Id = 61, Text = "شبابيك", QuestionId = 21 },
                new Option { Id = 62, Text = "سلالم", QuestionId = 21 },

                // Q22
                new Option { Id = 63, Text = "داخلي", QuestionId = 22 },
                new Option { Id = 64, Text = "خارجي", QuestionId = 22 },

                // Q23
                new Option { Id = 65, Text = "مائي", QuestionId = 23 },
                new Option { Id = 66, Text = "حراري", QuestionId = 23 },
                new Option { Id = 67, Text = "صوتي", QuestionId = 23 },

                // Q24
                new Option { Id = 68, Text = "سطح", QuestionId = 24 },
                new Option { Id = 69, Text = "حمام", QuestionId = 24 },
                new Option { Id = 70, Text = "جدران", QuestionId = 24 },

                // Q25
                new Option { Id = 71, Text = "زراعة", QuestionId = 25 },
                new Option { Id = 72, Text = "قص", QuestionId = 25 },
                new Option { Id = 73, Text = "تنظيف", QuestionId = 25 },

                // Q26
                new Option { Id = 74, Text = "صغيرة", QuestionId = 26 },
                new Option { Id = 75, Text = "متوسطة", QuestionId = 26 },
                new Option { Id = 76, Text = "كبيرة", QuestionId = 26 },

                // Q27
                new Option { Id = 77, Text = "غرفة نوم", QuestionId = 27 },
                new Option { Id = 78, Text = "صالة", QuestionId = 27 },
                new Option { Id = 79, Text = "مطبخ", QuestionId = 27 },

                // Q28
                new Option { Id = 80, Text = "بلاستيك", QuestionId = 28 },
                new Option { Id = 81, Text = "زيت", QuestionId = 28 },
                new Option { Id = 82, Text = "لاكيه", QuestionId = 28 },

                // Q29
                new Option { Id = 83, Text = "كهرباء", QuestionId = 29 },
                new Option { Id = 84, Text = "مياه", QuestionId = 29 },
                new Option { Id = 85, Text = "تكييف", QuestionId = 29 },

                // Q30
                new Option { Id = 86, Text = "منخفضة", QuestionId = 30 },
                new Option { Id = 87, Text = "متوسطة", QuestionId = 30 },
                new Option { Id = 88, Text = "عالية", QuestionId = 30 }
            );
        }


    }
}