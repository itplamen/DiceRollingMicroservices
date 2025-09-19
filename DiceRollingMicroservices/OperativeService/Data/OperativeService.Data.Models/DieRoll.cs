namespace OperativeService.Data.Models
{
    public class DieRoll : BaseModel
    {
        public int Result { get; set; }

        public string DieId { get; set; }
    }
}
