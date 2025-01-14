using LiberacionProductoWeb.Data.Repository;
using LiberacionProductoWeb.Models.CheckListViewModels;
using LiberacionProductoWeb.Models.ConditioningOrderViewModels;
using LiberacionProductoWeb.Models.DataBaseModels;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Services
{
    public class CheckListService : ICheckListService
    {
        private readonly ICheckListPipeRecordAnswerRepository _checkListPipeRecordAnswerRepository;
        private readonly ICheckListPipeDictiumAnswerRepository _checkListPipeDictiumAnswerRepository;
        private readonly ILogger<CheckListService> _logger;
        public CheckListService(ICheckListPipeRecordAnswerRepository checkListPipeRecordAnswerRepository, ILogger<CheckListService> logger, ICheckListPipeDictiumAnswerRepository checkListPipeDictiumAnswerRepository)
        {
            this._checkListPipeRecordAnswerRepository = checkListPipeRecordAnswerRepository;
            this._logger = logger;
            this._checkListPipeDictiumAnswerRepository = checkListPipeDictiumAnswerRepository;
        }

        public async Task<CheckListPipeDictiumAnswer> GetCheckListCloseOk()
        {
            var data = new CheckListPipeDictiumAnswer();
            try
            {
                data = (await _checkListPipeDictiumAnswerRepository.
                    GetAsync(x => x.StatusTwo.Equals(CheckListType.CloseOk.Value)
                    && x.Step.Equals(CheckListGeneralViewModelCheckListStep.Two.Value)
                    && x.InCompliance.Value))
                    .OrderBy(x => x.Date).LastOrDefault();

            }
            catch (Exception ex)
            {
                //_logger.LogInformation("Ocurrio un errorGetCheckListCloseOk " + ex);
                _logger.LogError(ex, "Error.");
            }
            return data;
        }
    }
}
