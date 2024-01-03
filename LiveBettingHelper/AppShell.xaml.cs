﻿using LiveBettingHelper.Views;

namespace LiveBettingHelper;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(CountrySelectorPage), typeof(CountrySelectorPage));
        Routing.RegisterRoute(nameof(LeagueSelectorPage), typeof(LeagueSelectorPage));
    }
}
