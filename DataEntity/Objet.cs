namespace DataEntity
{
    public abstract class Objet
    {
       public int Id { get; set; }
       public string Nom { get; set; } = string.Empty;
       public int Valeur { get; set; }
       public int IntelligenceRequise { get; set; }
       public EffetType Effet { get; set; }
       public int EffetValeur { get; set; }
       public ERarete Rarete { get; set; }

    }
}
