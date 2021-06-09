using System;
using System.Collections.Generic;
using System.Linq;

namespace TPMoyennes
{
    class Program
    {
        static void Main(string[] args)
        {
            // Création d'une classe
            Classe sixiemeA = new Classe("6eme A");
            // Ajout des élèves à la classe
            sixiemeA.ajouterEleve("Jean", "RAGE");
            sixiemeA.ajouterEleve("Paul", "HAAR");
            sixiemeA.ajouterEleve("Sibylle", "BOQUET");
            sixiemeA.ajouterEleve("Annie", "CROCHE");
            sixiemeA.ajouterEleve("Alain", "PROVISTE");
            sixiemeA.ajouterEleve("Justin", "TYDERNIER");
            sixiemeA.ajouterEleve("Sacha", "TOUILLE");
            sixiemeA.ajouterEleve("Cesar", "TICHO");
            sixiemeA.ajouterEleve("Guy", "DON");
            // Ajout de matières étudiées par la classe
            sixiemeA.ajouterMatiere("Francais");
            sixiemeA.ajouterMatiere("Anglais");
            sixiemeA.ajouterMatiere("Physique/Chimie");
            sixiemeA.ajouterMatiere("Histoire");
            Random random = new Random();
            // Ajout de 5 notes à chaque élève et dans chaque matière
            for (int ieleve = 0; ieleve < sixiemeA.eleves.Count; ieleve++)
            {
                for (int matiere = 0; matiere < sixiemeA.matieres.Count; matiere++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        sixiemeA.eleves[ieleve].ajouterNote(new Note(matiere, (float)((6.5 +
                       random.NextDouble() * 34)) / 2.0f));
                        // Note minimale = 3
                    }
                }
            }

            Eleve eleve = sixiemeA.eleves[6];
            // Afficher la moyenne d'un élève dans une matière
            Console.Write(eleve.prenom + " " + eleve.nom + ", Moyenne en " + sixiemeA.matieres[1] + " : " +
            eleve.Moyenne(1) + "\n");
            // Afficher la moyenne générale du même élève
            Console.Write(eleve.prenom + " " + eleve.nom + ", Moyenne Generale : " + eleve.Moyenne() + "\n");
            // Afficher la moyenne de la classe dans une matière
            Console.Write("Classe de " + sixiemeA.nomClasse + ", Moyenne en " + sixiemeA.matieres[1] + " : " +
            sixiemeA.Moyenne(1) + "\n");
            // Afficher la moyenne générale de la classe
            Console.Write("Classe de " + sixiemeA.nomClasse + ", Moyenne Generale : " + sixiemeA.Moyenne() + "\n");
            Console.Read();
        }
    }
}
// Classes fournies par HNI Institut
class Note
{
    public int matiere { get; private set; }
    public float note { get; private set; }
    public Note(int m, float n)
    {
        matiere = m;
        note = n;
    }
}

class Eleve
{
    public string prenom { get; private set; }
    public string nom { get; private set; }
    public List<Note> notes { get; private set; }
    private int counterNote = 0;
    private readonly int counterNoteMax = 200;
    public Eleve(string Prenom, string Nom)
    {
        prenom = Prenom;
        nom = Nom;
        notes = new List<Note>();
    }
    public void ajouterNote (Note note)
    {
        counterNote++;
        if (counterNote <= counterNoteMax)
        {
            notes.Add(note);
        }
        else
        {
            Console.WriteLine("Vous avez atteint la limite de {0} d'ajouts des notes pour l'élève {1} {2}. L'ajout d'une " + counterNote + " e note est interdit.", counterNoteMax, prenom, nom);
        }
    }
    //Moyenne d'un élève dans une matière
    public float Moyenne(int matiere)
    {
        float sommeNote = 0;
        int counterNote = 0;
        foreach (Note noteEleve in notes)
        {
            if (noteEleve.matiere == matiere)
            {
                sommeNote += noteEleve.note;
                counterNote++;
            }
        }
        double moyenne = Math.Truncate(sommeNote / counterNote * 100) / 100;
        return (float)moyenne;
    }
    // Moyenne générale d'un élève
    public float Moyenne()
    {
        float sommeNote = 0;
        int counterNote = 0;
        foreach (Note noteEleve in notes)
        {
            sommeNote += noteEleve.note;
            counterNote++;
        }
        double moyenne = Math.Truncate(sommeNote / counterNote * 100) / 100;
        return (float)moyenne;
    }
}

class Classe
{
    public string nomClasse { get; private set; }
    public List<Eleve> eleves { get; private set; }
    public List<string> matieres { get; private set; }
    private int counterEleve = 0;
    private readonly int counterEleveMax = 30;
    private int counterMatiere = 0;
    private readonly int counterMatiereMax = 10;
    public Classe(string NomClasse)
    {
        nomClasse = NomClasse;
        eleves = new List<Eleve>();
        matieres = new List<string>();
    }
    public void ajouterEleve(string prenom, string nom)
    {
        counterEleve++;
        if (counterEleve <= counterEleveMax)
        {
            Eleve eleve = new Eleve(prenom, nom);
            eleves.Add(eleve);
        }
        else
        {
            Console.WriteLine("Vous avez atteint la limite de {0} d'ajouts des élèves pour la classe {1}. L'ajout d'un " + counterEleve + " e élève est interdit.", counterEleveMax, nomClasse);
        }
    }
    public void ajouterMatiere(string nomMatiere)
    {
        counterMatiere++;
        if (counterMatiere <= counterMatiereMax)
        {
            matieres.Add(nomMatiere);
        }
        else
        {
            Console.WriteLine("Vous avez atteint la limite de {0} d'ajouts des matières pour la classe {1}. L'ajout d'une " + counterMatiere + " e matière est interdit.", counterMatiereMax, nomClasse);
        }
    }
    public int GetNombreMatiere()
    {
        return matieres.Count;
    }
    //Moyenne de la classe dans une matière
    public float Moyenne(int matiere)
    {
        float sommeNote = 0;
        int counterEleve = 0;
        foreach (Eleve eleve in eleves)
        {
            sommeNote += eleve.Moyenne(matiere);
            counterEleve++;
        }
        double moyenne = Math.Truncate(sommeNote / counterEleve * 100) / 100;
        return (float)moyenne;
    }
    // Moyenne générale de la classe
    public float Moyenne()
    {
        float sommeMoyenne = 0;
        int counterMoyenne = 0;
        for(int matiere = 0; matiere < matieres.Count; matiere++)
        {
            sommeMoyenne += Moyenne(matiere);
            counterMoyenne++;
        }
        double moyenne = Math.Truncate(sommeMoyenne / counterMoyenne * 100) / 100;
        return (float)moyenne;
    }
}

