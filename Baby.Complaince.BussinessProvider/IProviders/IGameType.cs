using Baby.Complaince.Entities.Dashboard;
using Baby.Complaince.Entities.RequestModels;
using Baby.Complaince.Entities.ResponseModel;
using Baby.Complaince.Entities.GameType;
using Baby.Complaince.Framework;
using System.Collections.Generic;
using System.Data;

namespace Baby.Complaince.BussinessProvider.IProviders
{
    public interface IGameType
    {
        List<GameType> GetGameType();
        List<GameCategory> GetAllGameCategory(long GameTypeId);
        List<GameSubCategory> GetAllSubGameCategory(long GameCategoryId);
    }
}
