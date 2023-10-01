namespace API.Models;

public class Reservations
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int RoomId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public DateTime ReservationDate { get; set; }
    public Decimal TotalPrice { get; set; }
    public bool ReservationReviewed { get; set; }
    public bool ReservationConfirmed { get; set; }
    public bool ReservationCanceled { get; set; }
    public bool ReservationPaid { get; set; }
}