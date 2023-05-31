using movie.Models.Ticket;

namespace movie.Models.Movie;

public class MovieModel : BaseModel
{
    public string Name { get; set; }
    public string Image { get; set; }
    public int Price { get; set; }

    public List<TicketModel> Tickets = new List<TicketModel>();

    public MovieModel(string name, string image)
    {
        this.Id = Guid.NewGuid().ToString();
        this.Name = name;
        this.Image = image;
        this.Price = new Random().Next(100, 200);
    }

    public MovieModel()
    {
    }

}