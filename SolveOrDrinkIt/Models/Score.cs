//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SolveOrDrinkIt.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Score
    {
        public int id { get; set; }

        public int gameId { get; set; }
        [System.ComponentModel.DisplayName("Username")]
        public string userId { get; set; }
        [System.ComponentModel.DisplayName("Score")]
        public int score1 { get; set; }
    
        public virtual Game Game { get; set; }
    }
}
