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
    public ReservationStatus ReservationStatus { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}