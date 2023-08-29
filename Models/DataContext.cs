#nullable disable
using Bogus;
using GraduationThesis_CarServices.Encrypting;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Randomly;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Garage> Garages { get; set; }
        public DbSet<Product> Products { get; set; }
        //public DbSet<Report> Reports { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<BookingDetail> BookingDetails { get; set; }
        public DbSet<GarageDetail> GarageDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<GarageMechanic> GarageMechanics { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<ServiceDetail> ServiceDetails { get; set; }
        public DbSet<BookingMechanic> BookingMechanics { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            OneToOneRelationship(modelBuilder);
            MultipleCascadePathFix(modelBuilder);
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Randomizer.Seed = new Random(200);
            SeedRoleData(modelBuilder);
            SeedCategoryData(modelBuilder);
            SeedServiceData(modelBuilder);
            SeedServiceDetailData(modelBuilder);
            SeedProductData(modelBuilder);
            SeedRandomGarageData(modelBuilder);
            SeedRandomUserData(modelBuilder);
            // SeedRandomCarData(modelBuilder);
            // SeedRandomCouponData(modelBuilder);
            // SeedRandomReviewData(modelBuilder);
            // SeedRandomBookingData(modelBuilder);
            // SeedRandomReportData(modelBuilder);
            watch.Stop();
            Console.WriteLine($"Total run time: {watch.ElapsedMilliseconds}");
        }

        private static void OneToOneRelationship(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Booking>()
            // .HasOne(b => b.Report).WithOne(r => r.Booking)
            // .HasForeignKey<Report>(e => e.ReportId)
            //.OnDelete(DeleteBehavior.Cascade)
            ;

            modelBuilder.Entity<User>()
            .HasOne(b => b.Customer).WithOne(r => r.User)
            .HasForeignKey<Customer>(e => e.CustomerId)
            //.OnDelete(DeleteBehavior.Cascade)
            ;


            modelBuilder.Entity<User>()
            .HasOne(b => b.Mechanic).WithOne(r => r.User)
            .HasForeignKey<Mechanic>(e => e.UserId)
            //.OnDelete(DeleteBehavior.Cascade)
            ;

            // modelBuilder.Entity<Service>().OwnsOne(x => x.ServiceGroup);
        }

        private static void MultipleCascadePathFix(ModelBuilder modelBuilder)
        {
            //path from User to Review
            modelBuilder.Entity<User>()
            .HasMany(u => u.Garages)
            .WithOne(u => u.User)
            .OnDelete(DeleteBehavior.Restrict);
        }

        private readonly DateTime now = DateTime.Now;

        private static void SeedRoleData(ModelBuilder modelBuilder)
        {
            var list = new List<Role>()
            {
                new Role{RoleId=1, RoleName="Customer"},
                new Role{RoleId=2, RoleName="Manager"},
                new Role{RoleId=3, RoleName="Mechanic"},
                new Role{RoleId=4, RoleName="Admin"},
                new Role{RoleId=5, RoleName="Staff"},
            };

            modelBuilder.Entity<Role>().HasData(list);
        }

        private void SeedCategoryData(ModelBuilder modelBuilder)
        {
            var list = new List<Category>{
                new Category{CategoryId=1, CategoryName="Sản phẩm vệ sinh", CreatedAt=now, CategoryStatus=Status.Activate},
                new Category{CategoryId=2, CategoryName="Sản phẩm nâng cấp", CreatedAt=now, CategoryStatus=Status.Activate}
            };

            modelBuilder.Entity<Category>().HasData(list);
        }

        private void SeedServiceData(ModelBuilder modelBuilder)
        {
            var list = new List<Service>{
                //GÓI DỊCH VỤ VỆ SINH + BẢO DƯỠNG
                new Service{ServiceId=1, ServiceName="Rửa xe + hút bụi + xịt gầm",
                    ServiceImage="https://voxeoto.vn/wp-content/uploads/2020/07/1.jpg",
                    ServiceDetailDescription="Quy trình rửa xe gồm 11 bước nhầm bảo vệ tuyệt đối lớp sơn xe của khách hàng, đồng thời mang lại vẻ ngoài sáng bóng sau mỗi lần rửa xe tại MeCar.", ServiceDuration=1,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Time,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=2, ServiceName="Tẩy nhựa đường",
                    ServiceImage="https://haoquangauto.com/images/ckeditor/images/tay-nhua-duong-go-vap.jpg?1",
                    ServiceDetailDescription="Tẩy nhựa đường là một sản phẩm được sử dụng để loại bỏ vết nhựa đường, dầu mỡ, và bụi bẩn trên bề mặt.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Time,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=3, ServiceName="Tẩy ố kính",
                    ServiceImage="https://titiautocare.com/wp-content/uploads/2023/06/tay-o-kinh-16507832201601599854.jpg",
                    ServiceDetailDescription="Tẩy ố kính là một quy trình giúp loại bỏ các vết ố, bụi bẩn, và mảng cứng trên bề mặt của kính.", ServiceDuration=1,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Time,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=4, ServiceName="Vệ Sinh + Bảo dưỡng khoang động cơ",
                    ServiceImage="https://thanhphongauto.com/wp-content/uploads/2019/09/ve-sinh-khoang-dong-co.jpg",
                    ServiceDetailDescription="Vệ sinh và bảo dưỡng khoang động cơ là quá trình quan trọng để đảm bảo hoạt động hiệu quả và độ bền của động cơ xe.", ServiceDuration=2,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=5, ServiceName="Vệ Sinh + Bảo dưỡng nội thất",
                    ServiceImage="https://autowash.vn/wp-content/uploads/2020/08/su-dung-may-hut-bui-de-lam-sach-bui-ban-o-moi-noi-ben-trong-xe.jpg",
                    ServiceDetailDescription="Vệ sinh và bảo dưỡng nội thất là quá trình dọn dẹp và bảo quản các bộ phận nội thất trong một không gian.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=6, ServiceName="Vệ sinh nội soi hệ thống lạnh",
                    ServiceImage="https://danchoioto.vn/wp-content/uploads/2021/02/ve-sinh-dan-lanh-o-to.jpg",
                    ServiceDetailDescription="Vệ sinh nội soi hệ thống lạnh là quá trình loại bỏ bụi bẩn, vi khuẩn và chất lỏng tích tụ trong hệ thống làm lạnh.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=7, ServiceName="Vệ sinh kim phun xăng",
                    ServiceImage="https://danchoioto.vn/wp-content/uploads/2021/02/nen-ve-sinh-kim-phun-o-to-sau-moi-15-000-20-000-km-van-hanh.jpg",
                    ServiceDetailDescription="Vệ sinh kim phun xăng là quá trình làm sạch và bảo dưỡng các bộ phận liên quan đến hệ thống phun nhiên liệu của động cơ.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=8, ServiceName="Vệ sinh kim phun dầu",
                    ServiceImage="https://danchoioto.vn/wp-content/uploads/2021/02/ve-sinh-kim-phun-bang-dung-dich-ve-sinh-kim-phun-buong-dot-o-to-rat-tien-va-don-gian.jpg.webp",
                    ServiceDetailDescription="Việc vệ sinh kim phun dầu là quá trình loại bỏ bụi bẩn, cặn dầu và các tạp chất khác khỏi bề mặt kim phun dầu để đảm bảo hoạt động hiệu quả của hệ thống nạp nhiên liệu.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=9, ServiceName="Diệt khuẩn Demi",
                    ServiceImage="https://baohaauto.vn/wp-content/uploads/may-khu-mui-diet-khuan-o-to-tamako-7-1.jpg",
                    ServiceDetailDescription="Diệt khuẩn Demi là một loại sản phẩm hoặc chất liệu được sử dụng để tiêu diệt hoặc làm giảm tác động của vi khuẩn hoặc vi rút.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Time,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=10, ServiceName="Diệt khuẩn khử mùi nội thất",
                    ServiceImage="https://tearu.com.vn/wp-content/uploads/2019/12/dung-dich-khu-mui-noi-that-o-to-3.jpg",
                    ServiceDetailDescription="Diệt khuẩn khử mùi nội thất là quá trình loại bỏ vi khuẩn và mùi hôi từ các bề mặt và không khí trong không gian nội thất.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=11, ServiceName="Vệ sinh két nước ô tô.",
                    ServiceImage="https://hyundaiphanthiet.vn/wp-content/uploads/2022/02/nuoc-lam-mat-1.jpg",
                    ServiceDetailDescription="Vệ sinh két nước ô tô là quá trình làm sạch và bảo dưỡng hệ thống két nước trong ô tô.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=12, ServiceName="Vệ sinh buồng đốt",
                    ServiceImage="https://i.ytimg.com/vi/HW2NL3jfwko/maxresdefault.jpg",
                    ServiceDetailDescription="Vệ sinh buồng đốt là quá trình làm sạch và bảo dưỡng buồng đốt trong các thiết bị đốt cháy, như lò sưởi, máy nhiệt, hay lò hơi.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=13, ServiceName="Vệ sinh họng ga+ bướm ga+ van EGR",
                    ServiceImage="https://phutungsuzuki.net/wp-content/uploads/2020/09/ve-sinh-buom-ga-710x400.jpg",
                    ServiceDetailDescription="Vệ sinh họng ga, bướm ga, và van EGR là quá trình làm sạch các phần của hệ thống ga và khí thải của xe ô tô.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=14, ServiceName="Vệ sinh, bảo dưỡng thắng",
                    ServiceImage="https://danchoioto.vn/wp-content/uploads/2021/01/su-dung-dung-dich-ve-sinh-thang-dia-chuyen-dung-xit-rua-lan-luot-cac-bo-phan.jpg",
                    ServiceDetailDescription="Vệ sinh, bảo dưỡng thắng là quá trình duy trì và bảo quản hệ thống thắng trên một phương tiện, như xe hơi hoặc xe máy, để đảm bảo rằng hệ thống thắng hoạt động an toàn và hiệu quả.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=15, ServiceName="Vệ sinh nội soi dàn lạnh",
                    ServiceImage="https://otohathanh.com/upload/images/noi-soi-dieu-hoa-o-to.jpg",
                    ServiceDetailDescription="Vệ sinh nội soi dàn lạnh là quá trình làm sạch và bảo dưỡng hệ thống nội soi dàn lạnh. Nội soi dàn lạnh là một phần quan trọng trong hệ thống làm lạnh của máy lạnh hoặc thiết bị điều hòa không khí", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                //GÓI DỊCH VỤ NGOẠI THẤT
                new Service{ServiceId=16, ServiceName="Phủ Nano",
                    ServiceImage="https://thanhphongauto.com/wp-content/uploads/2019/09/2-4.jpg",
                    ServiceDetailDescription="phủ bóng Nano là việc phủ lên bề mặt lớp sơn xe một lớp dung dịch có thành phần chính là các tinh thể có gốc hữu cơ với kích thước siêu nhỏ dạng Nano.", ServiceDuration=4,
                    ServiceGroup = ServiceGroup.PackageExterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=17, ServiceName="Phủ Ceramic 9H",
                    ServiceImage="https://brodetailing.vn/wp-content/uploads/2017/12/9h.jpg",
                    ServiceDetailDescription="Ceramic Pro 9H là lớp sơn phủ Nano- ceramic có độ bóng cao, hiệu ứng siêu kỵ nước, chống trầy xước, kháng hóa chất, chống tia cực tím, kháng nhiệt và chống Grafitti.", ServiceDuration=4,
                    ServiceGroup = ServiceGroup.PackageExterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=18, ServiceName="Phủ gầm gói tiêu chuẩn",
                    ServiceImage="https://danchoioto.vn/wp-content/uploads/2019/01/son-phu-gam-o-to-giup-han-che-hap-thu-nhiet-tu-mat-duong.jpg",
                    ServiceDetailDescription="Lớp phủ chống ăn mòn được áp dụng dưới phần dưới của xe, bao gồm cả khung gầm và các bộ phận khác như động cơ, hệ thống treo và ống xả.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageExterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=19, ServiceName="Phủ gầm gói cao cấp",
                    ServiceImage="https://danchoioto.vn/wp-content/uploads/2019/01/son-phu-gam-o-to-giup-han-che-hap-thu-nhiet-tu-mat-duong.jpg",
                    ServiceDetailDescription="Lớp phủ chống ăn mòn được áp dụng dưới phần dưới của xe, bao gồm cả khung gầm và các bộ phận khác như động cơ, hệ thống treo và ống xả.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageExterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=20, ServiceName="Dán phim Nano gói tiêu chuẩn",
                    ServiceImage="https://phoenixauto.vn/Uploads/origin/20200120/thumbnail_dan-phim-cach-nhiet-cho-porsche.jpg",
                    ServiceDetailDescription="Dán phim Nano chuyển sáng và chống chói lóa, đặc biệt, cơ chế dẫn điện chuyển đổi kim loại bằng oxy nitride tăng khả năng loại bỏ nhiệt nhiều hơn và bền hơn so với các loại phim cách nhiệt thông thường khác.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageExterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=21, ServiceName="Dán phim Nano gói cao cấp",
                    ServiceImage="https://phoenixauto.vn/Uploads/origin/20200120/thumbnail_dan-phim-cach-nhiet-cho-porsche.jpg",
                    ServiceDetailDescription="Dán phim Nano chuyển sáng và chống chói lóa, đặc biệt, cơ chế dẫn điện chuyển đổi kim loại bằng oxy nitride tăng khả năng loại bỏ nhiệt nhiều hơn và bền hơn so với các loại phim cách nhiệt thông thường khác.", ServiceDuration=2,
                    ServiceGroup = ServiceGroup.PackageExterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=22, ServiceName="Phim 3M- Llumar gói tiêu chuẩn",
                    ServiceImage="https://llumar.com.vn/wp-content/uploads/2022/08/phim-cach-nhiet-LLumar-USA-scaled.jpg",
                    ServiceDetailDescription="Phim 3M- Llumar phim cách nhiệt mang đến thẩm mỹ và khả năng duy trì các kết nối trên xe ổn định, không gây cản trở như sóng điện thoại, radio, GPS,… .", ServiceDuration=2,
                    ServiceGroup = ServiceGroup.PackageExterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=23, ServiceName="Phim 3M- Llumar gói cao cấp",
                    ServiceImage="https://llumar.com.vn/wp-content/uploads/2022/08/phim-cach-nhiet-LLumar-USA-scaled.jpg",
                    ServiceDetailDescription="Phim 3M- Llumar phim cách nhiệt mang đến thẩm mỹ và khả năng duy trì các kết nối trên xe ổn định, không gây cản trở như sóng điện thoại, radio, GPS,… .", ServiceDuration=2,
                    ServiceGroup = ServiceGroup.PackageExterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=24, ServiceName="Thay dầu, bộ lọc",
                    ServiceImage="https://storage.googleapis.com/f1-cms/2020/02/00cd4ca8-20200228_024616.jpg",
                    ServiceDetailDescription="Thay dầu, bộ lọc có vai trò lọc sạch các cặn bẩn và giữ lại mạt sắt đảm bảo dầu được lọc sạch giúp bảo vệ hệ thống bôi trơn, hạn chế hao mòn của các chi tiết trong động cơ.", ServiceDuration=1,
                    ServiceGroup = ServiceGroup.PackageExterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=25, ServiceName="Kiểm tra hệ thống điện, phanh, treo",
                    ServiceImage="https://thanhphongauto.com/wp-content/uploads/2020/03/hu-hong-cua-he-thong-treo.jpg",
                    ServiceDetailDescription="Kiểm tra hệ thống điện, phanh, treo là quá trình kiểm tra các bộ phận quan trọng trên ôtô để đảm bảo sự an toàn và hoạt động hiệu quả của xe.", ServiceDuration=2,
                    ServiceGroup = ServiceGroup.PackageExterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=26, ServiceName="Kiểm tra và thay bình ắc quy, bạc đạn, dây đai",
                    ServiceImage="https://danchoioto.vn/wp-content/uploads/2021/05/neu-dien-ap-binh-ac-quy-do-duoc-bang-hoac-lon-hon-12v-nghia-la-ac-quy-dang-binh-thuong.jpeg",
                    ServiceDetailDescription="Kiểm tra và thay bình ắc quy là quá trình kiểm tra tình trạng hoạt động của bình ắc quy và thay thế nếu cần.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageExterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                //GÓI DỊCH VỤ NỘI THẤT
                new Service{ServiceId=27, ServiceName="Áo ghế simili",
                    ServiceImage="https://thanhphongauto.com/wp-content/uploads/2019/09/5-dieu-can-biet-khi-boc-ao-ghe-simili-cho-xe-oto-02.jpg",
                    ServiceDetailDescription="Áo ghế Simili là một dạng vải tổng hợp bằng cách kết hợp chất liệu nhựa và sợi polyester. Được sản xuất để có độ bền cao, áo ghế simili thường có khả năng chống chịu mài mòn, chống thấm nước và dễ vệ sinh.", ServiceDuration=2,
                    ServiceGroup = ServiceGroup.PackageInterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=28, ServiceName="Thảm lót sàn",
                    ServiceImage="https://static.carmudi.vn/wp-content/uploads/2020-01/BCYttvBL1N.jpg",
                    ServiceDetailDescription="Thảm lót sàn được sử dụng để bảo vệ sàn ô tô khỏi bụi bẩn, nước, và các tác động bên ngoài khác.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageInterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=29, ServiceName="Mặt cốp + lưng ghế",
                    ServiceImage="https://katavina.com/uploaded/tin-tuc/tham-lot-cop-bang-da-pu.jpg",
                    ServiceDetailDescription="Thay thế Mặt cốp và lưng ghế ô tô là quá trình thay thế các bộ phận của cốp sau và lưng ghế trong xe ô tô.", ServiceDuration=2,
                    ServiceGroup = ServiceGroup.PackageInterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=30, ServiceName="La phông trần - bọc ni long",
                    ServiceImage="https://thanhphongauto.com/wp-content/uploads/2019/09/3-dieu-can-biet-ve-la-phong-tran-va-boc-ni-long-xe-%C3%B4t-02.png",
                    ServiceDetailDescription="La phông trần - bọc ni long là quá trình thêm một lớp ni da nhân tạo hoặc ni vinyl lên bề mặt của chiếc xe để bảo vệ nó khỏi các tác động từ môi trường như mưa, nắng, bụi bẩn, trầy xước.", ServiceDuration=2,
                    ServiceGroup = ServiceGroup.PackageInterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=31, ServiceName="Bọc da bò",
                    ServiceImage="https://hoangkims5.blob.core.windows.net/otohoangkimzag152hg18/boc-tran-da-kia-cerato.jpg",
                    ServiceDetailDescription="Bọc da bò là quá trình thêm một lớp da bò nhân tạo lên bề mặt của chiếc xe để bảo vệ nó khỏi các tác động từ môi trường như mưa, nắng, bụi bẩn, trầy xước và tăng thẩm mỹ cho xe.", ServiceDuration=2,
                    ServiceGroup = ServiceGroup.PackageInterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=32, ServiceName="Camera hành trình",
                    ServiceImage="https://danchoioto.vn/wp-content/uploads/2021/03/camera-hanh-trinh-oto.jpeg",
                    ServiceDetailDescription="Camera hành trình ô tô là một thiết bị ghi hình được gắn trên xe ô tô để ghi lại các sự kiện xảy ra trong quá trình lái xe.", ServiceDuration=2,
                    ServiceGroup = ServiceGroup.PackageInterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},
            };

            modelBuilder.Entity<Service>().HasData(list);
        }

        private static void SeedServiceDetailData(ModelBuilder modelBuilder)
        {
            var list = new List<ServiceDetail>();
            Random random = new();
            int y = 1;
            for (int i = 1; i <= 62; i++)
            {
                int million = random.Next(1, 4);
                int hundred = random.Next(1, 9);
                var price = decimal.Parse($"{million:N0}{hundred}00");
                list.Add(new ServiceDetail { ServiceDetailId = i, MinNumberOfCarLot = 4, MaxNumberOfCarLot = 5, ServicePrice = price, ServiceId = y });
                price += 200;
                list.Add(new ServiceDetail { ServiceDetailId = i + 1, MinNumberOfCarLot = 6, MaxNumberOfCarLot = 7, ServicePrice = price, ServiceId = y });
                i++;
                y++;
            }

            modelBuilder.Entity<ServiceDetail>().HasData(list);
        }

        private static void SeedProductData(ModelBuilder modelBuilder)
        {
            var list = new List<Product>{
                new Product{ProductId = 1, ProductName="Oil System Cleaner (Vệ sinh động cơ) 250ml",
                    ProductImage="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcROCCRqRZboIkPIiOOqYnyqRohBap1idqh_UQ&usqp=CAU",
                    ProductUnit=ProductUnit.Bottle, ProductPrice=28, ProductQuantity=100, ProductStatus=Status.Activate, ServiceId=4, CategoryId=1},

                new Product{ProductId = 2, ProductName="Fuel System Cleaner (Vệ sinh hệ thống xăng) 250ml",
                    ProductImage="https://down-vn.img.susercontent.com/file/0c60eba492310c8f98ef3b73011c659b",
                    ProductUnit=ProductUnit.Bottle, ProductPrice=(decimal)29.5, ProductQuantity=100, ProductStatus=Status.Activate, ServiceId=7, CategoryId=1},

                new Product{ProductId = 3, ProductName="Diesel System Cleaner (Vệ sinh hệ thống dầu) 350ml ",
                    ProductImage="https://salt.tikicdn.com/cache/w400/ts/product/eb/f5/9d/8357d880b50985b5d5bcef7715e02a27.jpg",
                    ProductUnit=ProductUnit.Bottle, ProductPrice=35, ProductQuantity=100, ProductStatus=Status.Activate, ServiceId=8, CategoryId=1},

                new Product{ProductId = 4, ProductName="Nano Engine Super Protection (Nano bảo vệ động cơ) 250ml",
                    ProductImage="https://pronanotex.com/wp-content/uploads/2016/11/NanoEngineSuperProtection-1.jpg",
                    ProductUnit=ProductUnit.Bottle, ProductPrice=(decimal)37.5, ProductQuantity=100, ProductStatus=Status.Activate, ServiceId=16, CategoryId=2},

                new Product{ProductId = 5, ProductName="Oxicat Oxygen Sensor & Catalytic (Vệ sinh cảm biến oxy và catalytic) 300ml",
                    ProductImage="https://m.media-amazon.com/images/I/61m+XWXThFL.jpg",
                    ProductUnit=ProductUnit.Bottle, ProductPrice=(decimal)29.5, ProductQuantity=100, ProductStatus=Status.Activate, ServiceId=11, CategoryId=1},

                new Product{ProductId = 6, ProductName="Throttle Body Cleaner (Vệ sinh họng ga) 280ml",
                    ProductImage="https://crcindustries.com.au/media/catalog/product/cache/628e20d88205b96f2e2f6bc42ae35fbb/6/e/6e33c61b31cd1e9013976610ca82741895d12a73db6d67342cf7b023e578aaa9.jpeg",
                    ProductUnit=ProductUnit.Bottle, ProductPrice=20, ProductQuantity=100, ProductStatus=Status.Activate, ServiceId=12, CategoryId=1},

                new Product{ProductId = 7, ProductName="Radiator Flush (Vệ sinh hệ thống làm mát) 300ml",
                    ProductImage="https://abro.com/wp-content/uploads/2020/07/RF-505-RADIATOR-FLUSH.jpg",
                    ProductUnit=ProductUnit.Bottle, ProductPrice=15, ProductQuantity=100, ProductStatus=Status.Activate, ServiceId=11, CategoryId=1},

                new Product{ProductId = 8, ProductName="Radiator conditioner (Vệ sinh điều hòa tản nhiệt)",
                    ProductImage="https://www.pro-tec-baltija.com/md5docupload/222fae770c434bc6fc23647307f6aab2.jpg",
                    ProductUnit=ProductUnit.Bottle, ProductPrice=21, ProductQuantity=100, ProductStatus=Status.Activate, ServiceId=15, CategoryId=1},

                new Product{ProductId = 9, ProductName="Oil System Cleaner (Vệ sinh động cơ) 350ml",
                    ProductImage="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcROCCRqRZboIkPIiOOqYnyqRohBap1idqh_UQ&usqp=CAU",
                    ProductUnit=ProductUnit.Bottle, ProductPrice=32, ProductQuantity=100, ProductStatus=Status.Activate, ServiceId=4, CategoryId=1},

                new Product{ProductId = 1, ProductName="Oil System Cleaner (Vệ sinh động cơ) 300ml",
                    ProductImage="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcROCCRqRZboIkPIiOOqYnyqRohBap1idqh_UQ&usqp=CAU",
                    ProductUnit=ProductUnit.Bottle, ProductPrice=30, ProductQuantity=100, ProductStatus=Status.Activate, ServiceId=4, CategoryId=1},

                new Product{ProductId = 1, ProductName="Fuel System Cleaner (Vệ sinh hệ thống xăng) 250ml",
                    ProductImage="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcROCCRqRZboIkPIiOOqYnyqRohBap1idqh_UQ&usqp=CAU",
                    ProductUnit=ProductUnit.Bottle, ProductPrice=34, ProductQuantity=100, ProductStatus=Status.Activate, ServiceId=7, CategoryId=1},
            };

            modelBuilder.Entity<Product>().HasData(list);
        }

        private void SeedRandomUserData(ModelBuilder modelBuilder)
        {
            var userList = new List<User>();
            var customerList = new List<Customer>();
            var mechanicList = new List<Mechanic>();

            var levelList = new string[] { MechanicLevel.Level1.ToString(), MechanicLevel.Level2.ToString(), MechanicLevel.Level3.ToString() };

            var customerFaker = new Faker<Customer>();
            var mechanicFaker = new Faker<Mechanic>();
            var userFaker = new Faker<User>();

            var encryptConfiguration = new EncryptConfiguration();
            encryptConfiguration.CreatePasswordHash("abc", out byte[] password_hash, out byte[] password_salt);

            int m = 0;
            for (int i = 1; i <= 290; i++)
            {
                switch (i)
                {
                    case <= 10:
                        customerFaker.RuleFor(c => c.CustomerId, i)
                            .RuleFor(c => c.CustomerAddress, "N/A")
                            .RuleFor(c => c.CustomerDistrict, "N/A")
                            .RuleFor(c => c.CustomerWard, "N/A")
                            .RuleFor(c => c.CustomerCity, "N/A");

                        customerList.Add(customerFaker.Generate());
                        break;
                    case > 10 and <= 290:
                        mechanicFaker.RuleFor(m => m.MechanicId, ++m)
                            .RuleFor(m => m.UserId, f => i)
                            .RuleFor(m => m.Level, f => f.PickRandom(levelList))
                            .RuleFor(m => m.MechanicStatus, f => MechanicStatus.Available);

                        mechanicList.Add(mechanicFaker.Generate());
                        break;
                }

                _ = userFaker.RuleFor(u => u.UserId, i)
                    .RuleFor(u => u.UserFirstName, f => f.Name.FirstName())
                    .RuleFor(u => u.UserLastName, f => f.Name.LastName())
                    .RuleFor(u => u.UserEmail, (f, u) => encryptConfiguration.Base64Encode(f.Internet.Email(u.UserFirstName, u.UserLastName)))
                    .RuleFor(u => u.PasswordHash, password_hash)
                    .RuleFor(u => u.PasswordSalt, password_salt)
                    .RuleFor(u => u.UserImage, f => f.Internet.Avatar())
                    .RuleFor(u => u.UserPhone, f => f.Random.Replace("+84#########"))
                    .RuleFor(u => u.UserGender, f => f.PickRandom<Gender>())
                    .RuleFor(u => u.UserBio, f => f.Lorem.Lines())
                    .RuleFor(u => u.UserStatus, Status.Activate)
                    .RuleFor(u => u.EmailConfirmed, 1)
                    .RuleFor(u => u.CreatedAt, now)
                    .RuleFor(u => u.RoleId, f =>
                    {
                        if (i <= 10)
                        {
                            return 1;
                        }
                        else
                        {
                            return 3;
                        }

                        /*switch (i)
                        {
                            case <= 20:
                                return 1; //Customer
                            case > 20 and <= 30:
                                return 5; //Staff
                            case > 30 and <= 249:
                                return 3; //Mechanic
                            case 250:
                                return 4; //Admin
                            default: return 2; //Manager 
                        }*/
                    })
                    /*.RuleFor(u => u.ManagerId, f =>
                    {
                        if (i > 20 && i <= 30)
                        {
                            return i - 20 + 50;
                        }
                        return null;
                    })*/
                    ;

                userList.Add(userFaker.Generate());
            }

            modelBuilder.Entity<Customer>().HasData(customerList);
            modelBuilder.Entity<Mechanic>().HasData(mechanicList);
            modelBuilder.Entity<User>().HasData(userList);
        }

        private void SeedRandomCarData(ModelBuilder modelBuilder)
        {
            var carList = new List<Car>();

            var carFaker = new Faker<Car>();

            for (int i = 1; i <= 25; i++)
            {
                carFaker.RuleFor(c => c.CarId, i)
                    .RuleFor(c => c.CarBrand, f => f.Vehicle.Manufacturer())
                    .RuleFor(c => c.CarLicensePlate, f => f.Random.Replace("##?-###.##"))
                    .RuleFor(c => c.CarFuelType, f => f.Vehicle.Fuel())
                    .RuleFor(c => c.CarDescription, f => f.Lorem.Paragraph())
                    .RuleFor(c => c.NumberOfCarLot, f => f.Random.Int(2, 9))
                    .RuleFor(c => c.CarBookingStatus, CarStatus.Available)
                    .RuleFor(c => c.CarStatus, Status.Activate)
                    .RuleFor(c => c.CreatedAt, now)
                    .RuleFor(c => c.CustomerId, f => f.Random.Int(1, 20));

                carList.Add(carFaker.Generate());
            }
            modelBuilder.Entity<Car>().HasData(carList);
        }

        private void SeedRandomGarageData(ModelBuilder modelBuilder)
        {
            var garageList = new List<Garage>();
            var garageDetailList = new List<GarageDetail>();
            var garageMechanicList = new List<GarageMechanic>();
            var lotList = new List<Lot>();

            var garageFaker = new Faker<Garage>();
            var garageDetailFaker = new Faker<GarageDetail>();
            var garageMechanicFaker = new Faker<GarageMechanic>();
            var lotFaker = new Faker<Lot>();

            for (int i = 1; i <= 14; i++)
            {
                var ran = RandomConfiguration.Location[i];

                garageFaker.RuleFor(g => g.GarageId, i)
                    .RuleFor(g => g.GarageName, "Me Garage. " + ran.Address)
                    .RuleFor(g => g.GarageAbout, f => f.Lorem.Paragraph())
                    .RuleFor(g => g.GarageImage, "https://firebasestorage.googleapis.com/v0/b/car-service-bf62f.appspot.com/o/logo.png?alt=media&token=ec9161af-b632-4d9e-a849-4e07dcce7ce3")
                    .RuleFor(g => g.GarageContactInformation, f => f.Random.Replace("+84#########"))
                    .RuleFor(g => g.OpenAt, "08:00 AM")
                    .RuleFor(g => g.CloseAt, "05:00 PM")
                    .RuleFor(g => g.GarageStatus, Status.Activate)
                    .RuleFor(g => g.CreatedAt, now)
                    //.RuleFor(g => g.UserId, f => /*f.Random.Int(21, 30)*/ 50 + i)
                    ;

                var garage = garageFaker.Generate();

                garage.GarageAddress = ran.Address;
                garage.GarageWard = ran.Ward;
                garage.GarageDistrict = ran.District;
                garage.GarageCity = ran.City;
                garage.GarageLatitude = ran.Latitude;
                garage.GarageLongitude = ran.Longitude;

                garageList.Add(garage);
            }

            int gradetailid = 1;
            for (int i = 1; i <= 14; i++)
            {
                Random random = new();
                int[] intArr = new int[] { 28, 30, 32 };
                int rIndex = random.Next(intArr.Length);
                int rNum = intArr[rIndex];

                int serviceid = 1;
                for (int j = 1; j <= rNum; j++)
                {
                    garageDetailFaker.RuleFor(s => s.GarageDetailId, gradetailid)
                    .RuleFor(s => s.ServiceId, serviceid)
                    .RuleFor(s => s.GarageId, i);

                    var garageDetail = garageDetailFaker.Generate();
                    garageDetailList.Add(garageDetail);
                    serviceid++;
                    gradetailid++;
                }
                /*if (!garageDetailList.Any(g => g.ServiceId == garageDetail.ServiceId &&
                g.GarageId == garageDetail.GarageId))
                {
                    garageDetailList.Add(garageDetail);
                    gd++;
                }
                else
                {
                    continue;
                }*/
            }

            int z = 0;
            int gm = 1;
            for (int i = 1; i <= 14; i++)
            {
                for (int y = 1; y <= 20; y++)
                {
                    garageMechanicFaker.RuleFor(w => w.GarageMechanicId, gm)
                    .RuleFor(w => w.GarageId, i)
                    .RuleFor(s => s.MechanicId, y + z);

                    var garageMechanic = garageMechanicFaker.Generate();
                    garageMechanicList.Add(garageMechanic);

                    gm++;
                }
                z += 20;
            }

            int lotid = 1;
            for (int i = 1; i <= 14; i++)
            {
                Random random = new();
                int[] intArr = new int[] { 4, 5 };
                int rIndex = random.Next(intArr.Length);
                int rNum = intArr[rIndex];

                for (int j = 1; j <= rNum; j++)
                {
                    lotFaker.RuleFor(l => l.LotId, lotid)
                    .RuleFor(l => l.LotNumber, f => f.Random.Replace("#?"))
                    .RuleFor(l => l.LotStatus, LotStatus.Free)
                    .RuleFor(l => l.GarageId, i);

                    lotList.Add(lotFaker.Generate());
                    lotid++;
                }
            }

            modelBuilder.Entity<GarageMechanic>().HasData(garageMechanicList);
            modelBuilder.Entity<Garage>().HasData(garageList);
            modelBuilder.Entity<GarageDetail>().HasData(garageDetailList);
            modelBuilder.Entity<Lot>().HasData(lotList);
        }

        private void SeedRandomReviewData(ModelBuilder modelBuilder)
        {
            var reviewList = new List<Review>();

            var reveiwFaker = new Faker<Review>();

            for (int i = 1; i <= 40; i++)
            {
                reveiwFaker.RuleFor(r => r.ReviewId, i)
                    .RuleFor(r => r.Rating, f => f.Random.Int(1, 5))
                    .RuleFor(r => r.Content, f => f.Lorem.Paragraph())
                    .RuleFor(r => r.ReviewStatus, Status.Activate)
                    .RuleFor(r => r.CustomerId, f => f.Random.Int(1, 20))
                    .RuleFor(r => r.GarageId, f => f.Random.Int(1, 14))
                    .RuleFor(r => r.CreatedAt, now);

                reviewList.Add(reveiwFaker.Generate());
            }
            modelBuilder.Entity<Review>().HasData(reviewList);
        }

        private void SeedRandomCouponData(ModelBuilder modelBuilder)
        {
            var couponList = new List<Coupon>();

            var couponFaker = new Faker<Coupon>();

            for (int i = 1; i <= 30; i++)
            {
                couponFaker.RuleFor(c => c.CouponId, i)
                    .RuleFor(c => c.CouponCode, f => f.Random.Replace("CARME###???#"))
                    .RuleFor(c => c.CouponDescription, f => f.Lorem.Paragraph())
                    .RuleFor(c => c.CouponType, /*f => f.PickRandom<CouponType>()*/ CouponType.FixedAmount)
                    .RuleFor(c => c.CouponValue, (f, g) =>
                    {
                        switch (g.CouponType)
                        {
                            case CouponType.Percent:
                                return f.Random.Int(1, 10);
                            case CouponType.FixedAmount:
                                return f.Random.Int(50, 100);
                        }
                        return 0;
                    })
                    .RuleFor(c => c.CouponStartDate, f => DateTime.Now)
                    .RuleFor(c => c.CouponEndDate, f => DateTime.Now.AddDays(f.Random.Int(5, 15)))
                    // .RuleFor(c => c.CouponMinSpend, f => f.Random.Int(10, 20))
                    // .RuleFor(c => c.CouponMaxSpend, f => f.Random.Int(60, 100))
                    .RuleFor(c => c.NumberOfTimesToUse, f => f.Random.Int(1, 10))
                    .RuleFor(c => c.CouponStatus, f => f.PickRandom<CouponStatus>())
                    .RuleFor(c => c.GarageId, f => f.Random.Int(1, 14))
                    .RuleFor(c => c.CreatedAt, now);

                couponList.Add(couponFaker.Generate());
            }
            modelBuilder.Entity<Coupon>().HasData(couponList);
        }

        private void SeedRandomBookingData(ModelBuilder modelBuilder)
        {
            var bookingList = new List<Booking>();
            var bookingDetailList = new List<BookingDetail>();
            var bookingMechanicList = new List<BookingMechanic>();

            var bookingFaker = new Faker<Booking>();
            var bookingDetailFaker = new Faker<BookingDetail>();
            var bookingMechanicFaker = new Faker<BookingMechanic>();

            for (int i = 1; i <= 50; i++)
            {
                bookingFaker.RuleFor(b => b.BookingId, i)
                    .RuleFor(b => b.BookingCode, f => f.Random.Replace("##?#???#?"))
                    .RuleFor(b => b.BookingTime, f => f.Date.Soon())
                    .RuleFor(b => b.PaymentMethod, f => "Cash")
                    .RuleFor(b => b.OriginalPrice, f => f.Random.Int(100, 1000))
                    .RuleFor(b => b.DiscountPrice, f => f.Random.Int(00, 30))
                    .RuleFor(b => b.TotalPrice, (f, g) => g.OriginalPrice - g.DiscountPrice)
                    .RuleFor(b => b.FinalPrice, (f, g) => g.TotalPrice)
                    // .RuleFor(b => b.PaymentStatus, f => f.PickRandom<PaymentStatus>())
                    .RuleFor(b => b.BookingStatus, f => f.PickRandom<BookingStatus>())
                    //.RuleFor(b => b.IsAccepted, f => true)
                    .RuleFor(b => b.CreatedAt, now)
                    .RuleFor(b => b.CarId, f => f.Random.Int(1, 25))
                    .RuleFor(b => b.GarageId, f => f.Random.Int(1, 14))
                    .RuleFor(b => b.IsAccepted, f => true)
                    .RuleFor(b => b.QrImage, f => "https://media.istockphoto.com/id/828088276/vi/vec-to/m%C3%A3-qr-minh-h%E1%BB%8Da.jpg?s=612x612&w=0&k=20&c=5qgn5q4gI0tuO6m_IEL90CpyOlifFa2ku0xA5gOWiOA=")
                    .RuleFor(b => b.CreatedAt, f => now);

                bookingList.Add(bookingFaker.Generate());
            }

            for (int i = 1; i <= 50; i++)
            {
                bookingDetailFaker.RuleFor(s => s.BookingDetailId, i)
                    .RuleFor(s => s.ProductPrice, f => f.Random.Int(50, 200))
                    .RuleFor(s => s.ServicePrice, f => f.Random.Int(50, 200))
                    .RuleFor(s => s.BookingServiceStatus, f => f.PickRandom<BookingServiceStatus>())
                    .RuleFor(s => s.BookingId, f => f.Random.Int(1, 15))
                    .RuleFor(s => s.ServiceDetailId, f => f.Random.Int(1, 32))
                    .RuleFor(s => s.ProductId, f => f.Random.Int(1, 8))
                    .RuleFor(s => s.CreatedAt, f => now);

                bookingDetailList.Add(bookingDetailFaker.Generate());
            }

            for (int i = 1; i <= 50; i++)
            {
                bookingMechanicFaker.RuleFor(b => b.BookingMechanicId, i)
                    .RuleFor(s => s.BookingMechanicStatus, f => Status.Activate)
                    .RuleFor(s => s.BookingId, f => f.Random.Int(1, 15))
                    .RuleFor(s => s.MechanicId, f => f.Random.Int(1, 18));

                bookingMechanicList.Add(bookingMechanicFaker.Generate());
            }

            modelBuilder.Entity<Booking>().HasData(bookingList);
            modelBuilder.Entity<BookingDetail>().HasData(bookingDetailList);
            modelBuilder.Entity<BookingMechanic>().HasData(bookingMechanicList);
        }

        // private void SeedRandomReportData(ModelBuilder modelBuilder)
        // {
        //     var reportFaker = new Faker<Report>();

        //     for (int i = 1; i <= 15; i++)
        //     {
        //         reportFaker.RuleFor(r => r.ReportId, i)
        //             .RuleFor(r => r.Date, f => f.Date.Past())
        //             .RuleFor(r => r.Notes, f => f.Lorem.Text())
        //             .RuleFor(r => r.Description, f => f.Lorem.Paragraph())
        //             .RuleFor(r => r.ReportStatus, f => Status.Activate)
        //             .RuleFor(r => r.CreatedAt, now);

        //         modelBuilder.Entity<Report>().HasData(reportFaker.Generate());
        //     }
        // }
    }
}