namespace WebService.Data.Model
{
    public class Congé
    {
        public int Id { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }

        public Boolean Verifie { get; set; }

        public string type { get; set; }
        public int AjoutPar { get; set; }

    }
}
