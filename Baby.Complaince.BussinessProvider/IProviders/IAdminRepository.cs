using Baby.Complaince.Entities.Dashboard;
using Baby.Complaince.Entities.RequestModels;
using Baby.Complaince.Entities.ResponseModel;
using Baby.Complaince.Entities.AdminModel;
using Baby.Complaince.Framework;
using System.Collections.Generic;
using System.Data;
using Baby.Complaince.Entities.GameType;
using Baby.Complaince.Entities.UserTransaction;

namespace Baby.Complaince.BussinessProvider.IProviders
{
    public interface IAdminRepository
    {
        DbOutput InsertWinningAmountForUser(Admin winnerUser);
        List<Admin> ListOfWinningUsers();
        List<Admin> ListOfWinningUsersById(Admin Id);
        List<Admin> ListOfUserWinningByUserId(Admin userWinbyUserId);
        DbOutput UpdateWinningUser(Admin updateWiinerUserDetails);
        //DbOutput DeleteWinningUser(Admin deleteWinnerUser);
        DbOutput UserDepositAmountApproved(Admin approvedAmount);
        DbOutput UserWithdrawAmountApproved(Admin approvedAmount);
        DbOutput DeclaredWinningNumber(Admin adminModel);
        List<AmountOnGameByGameType> ListOfAmountOnGameByGameType(long GameTypeId);
        List<UserWinDetails> GetUserListBasedOnGameWithBetNumberAndWinAmount();
        List<UserWalletBalance> GetUserListWalletBalance();
    }
}
