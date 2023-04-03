namespace ApplicationCore;

public class UsersSetting
{
    public NormalUserGiftPercentage NormalUserGiftPercentage { get; set; } = null!;
    public decimal PremiumUserGiftPercentage { get; set; }
    public decimal SuperUserGiftPercentage { get; set; }
}

public class NormalUserGiftPercentage
{
    public decimal MoreThan100Dolars { get; set; }
    public decimal LessThan100DolarsMoreThan10 { get; set; }
}
