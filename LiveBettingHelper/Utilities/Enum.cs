namespace LiveBettingHelper.Utilities
{
    public enum BetType
    {
        FirstHalfOver = 'F',
        SecondHalfOver = 'S'
    }

    public enum CheckType
    {
        NextMatchesCheck = 'N',
        CountryCheck = 'C',
        LeagueCheck = 'L',
        CompletedBetsCheck = 'M'
    }

    public enum SelectType
    {
        Selected = 'S',
        NotSelected = 'N',
        PartiallySelected = 'P'
    }

    public enum MatchStatus
    {
        /// <summary>
        /// Még nem kezdödött el.
        /// </summary>
        NS = 0,
        /// <summary>
        /// Folyamatban van.
        /// </summary>
        LIVE = 1,
        /// <summary>
        /// Félidő.
        /// </summary>
        HT = 2,
        /// <summary>
        /// A játék 90 perc után véget ért.
        /// </summary>
        FT = 3,
        /// <summary>
        /// Hosszabbítás (plusz 15-15 perc), kieséses meccseken fordul elő.
        /// </summary>
        ET = 4,
        /// <summary>
        /// Büntetőpárbaj.
        /// </summary>
        PEN_LIVE = 5,
        /// <summary>
        /// A játék hosszabitás (plusz 15-15 perc) után véget ért.
        /// </summary>
        AET = 6,
        /// <summary>
        /// A rendes játékidő véget ért, várakozás a hosszabbításra vagy a büntetőpárbajra.
        /// </summary>
        BREAK = 7,
        /// <summary>
        /// A játék büntetőpárbaj után véget ért.
        /// </summary>
        FT_PEN = 8,
        /// <summary>
        /// A játékot törölték.
        /// </summary>
        CANCL = 9,
        /// <summary>
        /// A játékot elhalasztották.
        /// </summary>
        POSTP = 10,
        /// <summary>
        /// A játék megszakadt. Például rossz időjárás miatt lehet.
        /// </summary>
        INT = 11,
        /// <summary>
        /// A játék félbeszakadt és egy későbbi időpontban vagy napon folytatódik.
        /// </summary>
        ABAN = 12,
        /// <summary>
        /// A játék felfüggesztésre került, és egy későbbi időpontban vagy napon folytatódik.
        /// </summary>
        SUSP = 13,
        /// <summary>
        /// A játéknak még nincs megerősített dátuma és időpontja. Később jelentik be.
        /// </summary>
        TBA = 14,
        /// <summary>
        /// A győztest külső tényezők döntik el.
        /// </summary>
        AWARDED = 15,
        /// <summary>
        /// A játék csúszik, így később fog kezdődni.
        /// </summary>
        DELAYED = 16,
        /// <summary>
        /// Győzelem odaítélése az egyik félnek, mivel nincsen ellenfél.
        /// </summary>
        WO = 17,
        /// <summary>
        /// Frissítésekre vár. Akkor fordulhat elő, ha kapcsolati probléma vagy ilyesmi van.
        /// </summary>
        AU = 18,
        /// <summary>
        /// Törölve. A játék már nem érhető el normál API-hívásokon keresztül.
        /// </summary>
        Deleted = 19,
        /// <summary>
        /// Hiba történt adat lekéréskor
        /// </summary>
        Error = 20
    }
}
