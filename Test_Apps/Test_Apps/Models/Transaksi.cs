using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test_Apps.Models
{
    public partial class Transaksi
    {
        [Required]
        [Display(Name = "Transaction Id")]
        public int TransactionId { get; set; }
        [Required]
        [Display(Name = "Account Id")]
        public int AccountId { get; set; }
        [Required]
        [Display(Name = "Transaction Date")]
        public DateTime TransactionDate { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "DebitCredit")]
        public string DebitCreditStatus { get; set; }
        [Required]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        public virtual Nasabah Account { get; set; }
    }

    public class Point
    {
        [Display(Name = "Account Id")]
        public int AccountId { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Point")]
        public int TPoint { get; set; }
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }
    }

    public class Report
    {
        [Display(Name = "Transaction Date")]
        public DateTime TransactionDate { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Debit")]
        public string Debit { get; set; }
        [Display(Name = "Credit")]
        public string Credit { get; set; }
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }
    }
}