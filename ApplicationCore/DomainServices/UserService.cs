using ApplicationCore.Entities.UserAggregate;
using ApplicationCore.Interfaces;
using Microsoft.Extensions.Options;

namespace ApplicationCore.DomainServices;

public class UserService : IUserService
{
    private readonly UsersSetting _usersSettingMonitor;

    public UserService(IOptionsMonitor<UsersSetting> usersSettingMonitor)
    {
        _usersSettingMonitor = usersSettingMonitor.CurrentValue;
    }

    public void AddGift(User user)
    {
        switch (user.Type)
        {
            case UserType.Normal:
                user.AddGift(CalculateNormalGift(user));                
                break;
            case UserType.Premium:
                user.AddGift(user.Money * _usersSettingMonitor.PremiumUserGiftPercentage);
                break;
            case UserType.SuperUser:
                user.AddGift(user.Money * _usersSettingMonitor.SuperUserGiftPercentage);
                break;
            default:
                break;
        }
    }

    private decimal CalculateNormalGift(User user)
    {
        if (user.Money > 100)
        {
            return user.Money * _usersSettingMonitor.NormalUserGiftPercentage.MoreThan100Dolars;
        }

        if (user.Money > 10 && user.Money < 100)
        {
            return user.Money * _usersSettingMonitor.NormalUserGiftPercentage.LessThan100DolarsMoreThan10;
        }

        return 0;
    }
}
