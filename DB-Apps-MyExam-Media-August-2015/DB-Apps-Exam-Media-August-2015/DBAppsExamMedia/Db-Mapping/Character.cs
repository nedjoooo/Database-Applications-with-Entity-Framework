//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Db_Mapping
{
    using System;
    using System.Collections.Generic;
    
    public partial class Character
    {
        public Character()
        {
            this.UsersGames = new HashSet<UsersGame>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> StatisticId { get; set; }
    
        public virtual Statistic Statistic { get; set; }
        public virtual ICollection<UsersGame> UsersGames { get; set; }
    }
}
