using LiveBettingHelper.Utilities;

namespace LiveBettingHelper.Components;

public partial class MyCheckBox : ContentView
{
    public static readonly BindableProperty BoxColorProperty = BindableProperty.Create(nameof(BoxColor), typeof(Color), typeof(MyCheckBox), default(Color));
    public Color BoxColor
    {
        get => (Color)GetValue(BoxColorProperty);
        set => SetValue(BoxColorProperty, value);
    }

    public static readonly BindableProperty CheckTypeProperty = BindableProperty.Create(nameof(CheckType), typeof(SelectType), typeof(MyCheckBox), default(SelectType), BindingMode.TwoWay);
    public SelectType CheckType
    {
        get => (SelectType)GetValue(CheckTypeProperty);
        set => SetValue(CheckTypeProperty, value);
    }

    public static readonly BindableProperty OnSelectCmdProperty = BindableProperty.Create(nameof(OnSelectCmd), typeof(Command), typeof(MyCheckBox), default(Command));
    public Command OnSelectCmd
    {
        get => GetValue(OnSelectCmdProperty) as Command;
        set => SetValue(OnSelectCmdProperty, value);
    }

    public static readonly BindableProperty OnSelectCmdParamProperty = BindableProperty.Create(nameof(OnSelectCmdParam), typeof(object), typeof(MyCheckBox), default(object));
    public object OnSelectCmdParam
    {
        get => GetValue(OnSelectCmdParamProperty) as object;
        set => SetValue(OnSelectCmdParamProperty, value);
    }

    public static readonly BindableProperty OnDeselectCmdProperty = BindableProperty.Create(nameof(OnDeselectCmd), typeof(Command), typeof(MyCheckBox), default(Command));
    public Command OnDeselectCmd
    {
        get => GetValue(OnDeselectCmdProperty) as Command;
        set => SetValue(OnDeselectCmdProperty, value);
    }

    public static readonly BindableProperty OnDeselectCmdParamProperty = BindableProperty.Create(nameof(OnDeselectCmdParam), typeof(object), typeof(MyCheckBox), default(object));
    public object OnDeselectCmdParam
    {
        get => GetValue(OnDeselectCmdParamProperty) as object;
        set => SetValue(OnDeselectCmdParamProperty, value);
    }

    public MyCheckBox()
    {
        InitializeComponent();
    }

    private void SelectionCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if ((SelectionCheckBox.IsChecked && CheckType != SelectType.Selected) || (!SelectionCheckBox.IsChecked && CheckType == SelectType.Selected))
        {
            CheckType = SelectionCheckBox.IsChecked ? SelectType.Selected : SelectType.NotSelected;
            if (SelectionCheckBox.IsChecked) OnSelectCmd.Execute(OnSelectCmdParam);
            else OnDeselectCmd.Execute(OnDeselectCmdParam);
        }
    }
}