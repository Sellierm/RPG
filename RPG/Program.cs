using System.Text;

namespace RPG
{
    internal class Program
    {
        // Point d'entrée principal de l'application RPG
        // Lance le jeu et initialise tous les systèmes
        static void Main(string[] args)
        {   
            Jeu jeu = new Jeu();
            jeu.Demarrer();
        }
    }
}
