using ApplicationCore;
using ApplicationCore.DomainServices;
using ApplicationCore.Entities.UserAggregate;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Users.UnitTests.DomainServices;

public class UserServiceTest
{
    [Theory]
    [InlineData(UserType.Normal, 101, 113.12)]
    [InlineData(UserType.Normal, 11, 19.8)]
    [InlineData(UserType.Normal, 100, 100)]
    [InlineData(UserType.Normal, 10, 10)]
    [InlineData(UserType.Premium, 10, 30)]
    [InlineData(UserType.SuperUser, 10, 12)]
    public void AddGift_AddsTheCorrectMoneyToTheUser(UserType userType, decimal money, decimal expectedValue)
    {
        // Arrange
        var user = new User("Juan", "Juan@marmol.com", "+5491154762312", "Peru 2464", userType, money);

        var service = new UserService(Mock.Of<IOptionsMonitor<UsersSetting>>(_ => _.CurrentValue == new UsersSetting
        {
            NormalUserGiftPercentage = new()
            {
                LessThan100DolarsMoreThan10 = (decimal)0.8,
                MoreThan100Dolars = (decimal)0.12
            },
            PremiumUserGiftPercentage = 2,
            SuperUserGiftPercentage = (decimal)0.20
        }));

        // Act
        service.AddGift(user);

        // Assert
        Assert.Equal(user.Money, expectedValue);
    }
}
