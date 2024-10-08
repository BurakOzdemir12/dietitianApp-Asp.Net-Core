namespace dietitianBackend.Entities
{
    public class Measurements
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int  Amount { get; set; }
/*        public string? Img { get; set; }
*/
        public int FoodId { get; set; }
        public virtual Food Food { get; set; }

    }
}
