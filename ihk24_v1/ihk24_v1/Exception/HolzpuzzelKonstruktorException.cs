using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Exception die geworfen wird, um die Erstellung eines unlgültiges Holzpuzzle zu verhinden
/// </summary>
    class HolzpuzzelKonstruktorException: Exception
    {
        public HolzpuzzelKonstruktorException(string message) : base(message)
        {

        }

    public HolzpuzzelKonstruktorException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

