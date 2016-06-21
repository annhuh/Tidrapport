using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tidrapport.Models
{
    public enum BalanceEffect
        {
            Ingen = 0,
            PlusPåÖvertid1 = 1,
            PlusPåÖvertid2 = 2,
            PlusPåÖvertid3 = 3,
            MinusPåÖvertid1 = 4,
            MinusPåÖvertid2 = 5,
            MinusPåÖvertid3 = 6,
            MinusPåNationaldagSaldo = 7,
            UttagBetaldSemesterdag = 8,
            UttagObetaldSemesterdag = 9,
            UttagSparadSemesterdag = 10
         }

    public enum TRStatus
    {
        Utkast = 1,
        Inlämnad = 2,
        Godkänd = 3,
        Returnerad = 4
    }

    public enum HolidayPeriod
    {
        [Display(Name = "1 Januari - 31 December")]
        Januari = 1,
        [Display(Name = "1 April - 31 Mars")]
        April = 2
    }
}