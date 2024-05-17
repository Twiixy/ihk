using ihk24_v1.Puzzle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ihk24_v1.Tests
{
    /// <summary>
    /// Klasse um die rotationen der Holzstreifen Klasse zu testen
    /// </summary>
    class HolzStreifenTester: Holzstreifen
    {
        public HolzStreifenTester(string id, List<int> element) : base(id, element) 
        {
            Console.WriteLine("Teste rotationen:");
            rotationsTests();

        }

        /// <summary>
        /// Tetstet die "rotieren" Methode der Basisklasse.
        /// </summary>
        private void rotationsTests()
        {
            bool isCorrect = true;
            base.Elemente=new List<int> { 1, 2, 3, 4, 0 };
            base.rotieren('x');
            if(!base.Elemente.SequenceEqual( new List<int> { 0,4,3,2,1})) {
                Console.WriteLine("Fehler bei x-Rotation");
                isCorrect = false;
            }
            base.rotieren('x');
            if (!base.Elemente.SequenceEqual(new List<int> { 1, 2, 3, 4, 0 }))
            {
                Console.WriteLine("Fehler bei x-Rotation");
                isCorrect = false;  
            }
            base.rotieren('y');
            if (!base.Elemente.SequenceEqual(new List<int> { 2, 1, 3, 4, 0 }))
            {
                Console.WriteLine("Fehler bei y-Rotation");
                isCorrect = false;
            }
            base.rotieren('y');
            if (!base.Elemente.SequenceEqual(new List<int> { 1, 2, 3, 4, 0 }))
            {
                Console.WriteLine("Fehler bei y-Rotation");
                isCorrect = false;
            }
            if(isCorrect) { Console.WriteLine("Rotationen funktionieren Fehlerfrei!"); }
            
        }
    }
}
