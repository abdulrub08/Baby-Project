using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Baby.Complaince.Framework;
using Baby.Complaince.Entities.RequestModels;
using Baby.Complaince.Entities.Dashboard;
using Baby.Complaince.Entities.ResponseModel;
using Baby.Complaince.BussinessProvider.IProviders;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Baby.Complaince.Entities.GameType;

namespace Baby.Complaince.DataAccess.Repository
{
    public class GameTypeRepository : BaseDAL, IGameType
    {
        private Database _dbContextDQCPRDDB;

        public GameTypeRepository()
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            _dbContextDQCPRDDB = factory.Create(C_Connection);
        }
        public List<GameType> GetGameType()
        {
            List<GameType> GameTypeList = new List<GameType>();
            DbCommand dbCommand = _dbContextDQCPRDDB.GetStoredProcCommand(DBConstraints.ALL_GAMETYPE);
            using (IDataReader reader = _dbContextDQCPRDDB.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    GameTypeList.Add(GenerateGameTypeDataReader(reader));
                }
            }
            return GameTypeList;
        }
        public List<GameCategory> GetAllGameCategory(long GameTypeId)
        {
            List<GameCategory> GameCategoryList = new List<GameCategory>();
            DbCommand dbCommand = _dbContextDQCPRDDB.GetStoredProcCommand(DBConstraints.ALL_GAMECATEGORY);
            _dbContextDQCPRDDB.AddInParameter(dbCommand, "GameTypeId", DbType.String, GameTypeId);            
            using (IDataReader reader = _dbContextDQCPRDDB.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    GameCategoryList.Add(GenerateGameCategoryDataReader(reader));
                }
            }
            return GameCategoryList;
        }
        public List<GameSubCategory> GetAllSubGameCategory(long GameCategoryId)
        {
            List<GameSubCategory> GameSubCategoryList = new List<GameSubCategory>();
            DbCommand dbCommand = _dbContextDQCPRDDB.GetStoredProcCommand(DBConstraints.ALL_GAMESUBCATEGORY);
            _dbContextDQCPRDDB.AddInParameter(dbCommand, "GameCategoryId", DbType.String, GameCategoryId);         
            using (IDataReader reader = _dbContextDQCPRDDB.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    GameSubCategoryList.Add(GenerateGameSubCategoryDataReader(reader));
                }
            }
            return GameSubCategoryList;
        }
        private GameType GenerateGameTypeDataReader(IDataReader reader)
        {
            GameType gameType = new GameType();
            gameType.GameTypeId = GetLongIntegerFromDataReader(reader, "GameTypeId");
            gameType.GameName = GetStringFromDataReader(reader, "GameName");
            gameType.StartDate = GetDateTimeSpanFromDataReader(reader, "StartDate");
            gameType.EndDate = GetDateTimeSpanFromDataReader(reader, "EndDate");
            gameType.IsActive = GetBooleanFromDataReader(reader, "isActive");
            gameType.Duration = GetIntegerFromDataReader(reader, "Duration");
            gameType.CreatedBy = GetStringFromDataReader(reader, "CreatedBy");
            gameType.UpdatedBy = GetStringFromDataReader(reader, "UpdatedBy");
            gameType.UpdatedDate = GetDateFromDataReader(reader, "UpdatedDate");
            gameType.CreatedDate = GetDateFromDataReader(reader, "CreatedDate");
            System.DateTime _current_date = GetDateTimeSpanFromDataReader(reader, "CurrentTime");
            System.DateTime _current_Time = new System.DateTime(gameType.StartDate.Year, gameType.StartDate.Month, gameType.StartDate.Day, _current_date.Hour, _current_date.Minute, _current_date.Second);
            System.DateTime _start_date = new System.DateTime(gameType.StartDate.Year, gameType.StartDate.Month, gameType.StartDate.Day, gameType.StartDate.Hour, gameType.StartDate.Minute, gameType.StartDate.Second);
            System.DateTime _end_date = new System.DateTime(gameType.EndDate.Year, gameType.EndDate.Month, gameType.EndDate.Day, gameType.EndDate.Hour, gameType.EndDate.Minute, gameType.EndDate.Second);
            #region 
            if (gameType.GameName.ToUpper()== "DESAWAR" || gameType.GameName.ToUpper() == "READY2ENJOY")
            {
                //_end_date = new System.DateTime(gameType.EndDate.Year, gameType.EndDate.Month, gameType.EndDate.Day+1, gameType.EndDate.Hour, gameType.EndDate.Minute, gameType.EndDate.Second);
                if (_current_Time < _start_date && _current_Time >= _end_date)
                {
                    gameType.Status = 0;
                }
                else
                {
                    gameType.Status = 1;
                }
            }
            else
            {
                if(_current_Time >= _start_date && _current_Time < _end_date)
                {
                    gameType.Status = 1;
                }
                else
                {
                    gameType.Status = 0;
                }
            }
            #endregion

            return gameType;
        }
        private GameCategory GenerateGameCategoryDataReader(IDataReader reader)
        {
            GameCategory gameType = new GameCategory();
            gameType.GameCategoryId = GetLongIntegerFromDataReader(reader, "GameCategoryId");
            gameType.GameTypeId = new GameType() { GameTypeId= GetLongIntegerFromDataReader(reader, "GameTypeId") };
            gameType.CategoryName = GetStringFromDataReader(reader, "CategoryName");
            gameType.bidRate = GetDecimalFromDataReader(reader, "bidRate");
            gameType.IsActive = GetBooleanFromDataReader(reader, "IsActive");
            //gameType.IsDeleted = GetBooleanFromDataReader(reader, "is_deleted");
            gameType.CreatedBy = GetStringFromDataReader(reader, "CreatedBy");
            gameType.UpdatedBy = GetStringFromDataReader(reader, "UpdatedBy");
            gameType.UpdatedDate = GetDateFromDataReader(reader, "UpdatedDate");
            gameType.CreatedDate = GetDateFromDataReader(reader, "CreatedDate");

            return gameType;
        }
        private GameSubCategory GenerateGameSubCategoryDataReader(IDataReader reader)
        {
            GameSubCategory gameType = new GameSubCategory();
            gameType.GameSubCategoryId = GetLongIntegerFromDataReader(reader, "GameSubCategoryId");
            gameType.GameCategeryId = new GameCategory() { GameCategoryId = GetLongIntegerFromDataReader(reader, "GameCategeryId") };
            gameType.SubCategoryName = GetStringFromDataReader(reader, "SubCategoryName");
            gameType.bidRate = GetDecimalFromDataReader(reader, "bidRate");
            gameType.IsActive = GetBooleanFromDataReader(reader, "IsActive");
            //gameType.IsDeleted = GetBooleanFromDataReader(reader, "is_deleted");
            //gameType.CreatedBy = GetStringFromDataReader(reader, "CreatedBy");
            //gameType.UpdatedBy = GetStringFromDataReader(reader, "UpdatedBy");
            //gameType.UpdatedDate = GetDateFromDataReader(reader, "UpdatedDate");
            //gameType.CreatedDate = GetDateFromDataReader(reader, "CreatedDate");

            return gameType;
        }     
    }
}
