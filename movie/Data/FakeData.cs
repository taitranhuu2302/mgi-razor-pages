using movie.Models.Movie;
using movie.Models.Room;
using movie.Models.Ticket;
using movie.Models.User;

namespace movie.Data;

public static class FakeData
{
    public static List<UserModel> Users;
    public static List<RoomModel> Rooms;
    public static List<MovieModel> Movies;

    static FakeData()
    {
        Users = new List<UserModel>()
        {
            new UserModel("Tran Huu Tai", "admin@gmail.com", "password", UserRole.Admin),
            new UserModel("New Name 1", "name2@gmail.com", "password", UserRole.User),
            new UserModel("New Name 2", "name3@gmail.com", "password", UserRole.User),
            new UserModel("New Name 3", "name4@gmail.com", "password", UserRole.User),
            new UserModel("New Name 4", "name5@gmail.com", "password", UserRole.User),
        };
        Rooms = new List<RoomModel>()
        {
            new RoomModel() { Name = "Room 1", Seats = 120 },
            new RoomModel() { Name = "Room 2", Seats = 120 },
            new RoomModel() { Name = "Room 3", Seats = 120 },
            new RoomModel() { Name = "Room 4", Seats = 120 },
            new RoomModel() { Name = "Room 5", Seats = 120 },
            new RoomModel() { Name = "Room 6", Seats = 120 },
            new RoomModel() { Name = "Room 7", Seats = 120 },
            new RoomModel() { Name = "Room 8", Seats = 120 },
        };

        Movies = new List<MovieModel>()
        {
            new MovieModel("LẬT MẶT 6: TẤM VÉ ĐỊNH MỆNH",
                "https://rapchieuphim.com/photos/movie/lat-mat-6-tam-ve-dinh-menh-poster.jpg"),
            new MovieModel("CHUYỆN XÓM TUI: CON NHÓT MÓT CHỒNG",
                "https://rapchieuphim.com/photos/movie/chuyen-xom-tui-con-nhot-mong-chong-poster.jpg"),
            new MovieModel("VÂY HÃM: KHÔNG LỐI THOÁT",
                "https://rapchieuphim.com/photos/movie/vay-ham-khong-loi-thoat-poster.jpg"),
            new MovieModel("NGƯỜI NHỆN DU HÀNH VŨ TRỤ NHỆN",
                "https://rapchieuphim.com/photos/movie/nguoi-nhen-du-hanh-vu-tru-nhen-poster.jpg"),
            new MovieModel("HOON PAYON: BÙA HÌNH NHÂN",
                "https://rapchieuphim.com/photos/9/hoon-payon-bua-hinh-nhan-thumb.jpg"),
            new MovieModel("ÔNG KẸ", "https://rapchieuphim.com/photos/movie/ong-ke-poster.jpg"),
            new MovieModel("NƠI TA GẶP NHAU", "https://rapchieuphim.com/photos/9/noi-ta-gap-nhau-thumb.jpg"),
            new MovieModel("VÂY HÃM: NGOÀI VÒNG PHÁP LUẬT",
                "https://rapchieuphim.com/photos/movie/vay-ham-ngoai-vong-phap-luat-poster.jpg"),
            new MovieModel("NÀNG TIÊN CÁ", "https://rapchieuphim.com/photos/movie/nang-tien-ca-poster.jpg"),
            new MovieModel("CHÀNG TRAI XINH ĐẸP CỦA TÔI: ĐỜI ĐỜI KIẾP KIẾP",
                "https://rapchieuphim.com/photos/9/chang-chang-xinh-dep-cua-toi-doi-doi-kiep-kiep-thumb.jpg"),
            new MovieModel("DORAEMON: NOBITA VÀ VÙNG ĐẤT LÝ TƯỞNG TRÊN BẦU TRỜI",
                "https://rapchieuphim.com/photos/movie/phim-dien-anh-doraemon-nobita-va-vung-dat-ly-tuong-tren-bau-troi-poster.jpg"),
            new MovieModel("TIỄN BIỆT CHỒNG YÊU", "https://rapchieuphim.com/photos/9/tien-biet-chong-yeu-thumb.jpg"),
            new MovieModel("TIẾNG GỌI ÂM BINH", "https://rapchieuphim.com/photos/9/tieng-goi-am-binh-thumb.jpg"),
            new MovieModel("NHỮNG KẺ THAO TÚNG", "https://rapchieuphim.com/photos/movie/nhung-ke-thao-tung-poster.jpg"),
            new MovieModel("CÔ BÉ CỨU HỎA", "https://rapchieuphim.com/photos/movie/co-be-cuu-hoa-poster.jpg"),
            new MovieModel("CÚ ÚP RỔ ĐẦU TIÊN", "https://rapchieuphim.com/photos/9/cu-up-ro-dau-tien-thumb.jpg"),
        };
        var random = new Random();
        foreach (var movieModel in Movies)
        {
            for (var i = 0; i < 30; i++)
            {
                movieModel.Tickets.Add(new TicketModel()
                {
                    Movie = movieModel,
                    Room = Rooms[random.Next(1, 8)]
                });
            }
        }
    }
}