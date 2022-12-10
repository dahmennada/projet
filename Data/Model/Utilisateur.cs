namespace WebService.Data.Model
{
    public class Utilisateur
    {


        public int Id { get; set; }
        public string Email { get; set; }

        public string MotDePasse { get; set; }

        public string Nom { get; set; }
        public string Prénom { get; set; }
        public int PhoneNumb { get; set; }
        public int SoldeCongé { get; set; }
        public int TempsTravail { get; set; }
        public string Role { get; set; }
    }
}
