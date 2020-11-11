using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SaskPartyDonors.Extensions;
using SaskPartyDonors.Filters;
using SaskPartyDonors.Services.Importers;
using SaskPartyDonors.Services.Importers.Dtos;

namespace SaskPartyDonors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private ILogger<ImportController> _logger;

        private readonly SaskCsvImporter _saskCsvImporter;

        private readonly AirtableCsvImporter _airtableCsvImporter;

        private readonly NpCsvImporter _npCsvImporter;

        public ImportController(ILogger<ImportController> logger,
            SaskCsvImporter saskCsvImporter,
            AirtableCsvImporter airtableCsvImporter,
            NpCsvImporter npCsvImporter)
        {
            _logger = logger;
            _saskCsvImporter = saskCsvImporter;
            _airtableCsvImporter = airtableCsvImporter;
            _npCsvImporter = npCsvImporter;
        }

#if DEBUG
        [HttpPost]
        [RequestSizeLimit(52428800)]
        [DisableFormModelBinding]
        [Route("saskCsv/recipient/{recipientId:Guid}/year/{year:int}", Name = nameof(ImportSaskCsv))]
        public async Task<ActionResult<ImportResultDto>> ImportSaskCsv(Guid recipientId, int year)
        {
            EnableSynchronousIO();

            try
            {

                if (!Request.HasFormContentType)
                {
                    return new UnsupportedMediaTypeResult();
                }

                // GetById a reader for multipart content in the request
                var multipartReader = Request.GetMultipartReader();
                var section = await multipartReader.ReadNextSectionAsync();

                // Multiple files/sections will not be saved. Only read the first one
                if (section == null)
                {
                    return BadRequest();
                }
                _saskCsvImporter.Year = year;
                return await _saskCsvImporter.ImportFromStream(section.Body, recipientId);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Sask import failed.");
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [RequestSizeLimit(52428800)]
        [DisableFormModelBinding]
        [Route("airtable/recipient/{recipientId:Guid}", Name = nameof(ImportAirtable))]
        public async Task<ActionResult<ImportResultDto>> ImportAirtable(Guid recipientId)
        {

            EnableSynchronousIO();

            try
            {
                if (!Request.HasFormContentType)
                {
                    return new UnsupportedMediaTypeResult();
                }

                // GetById a reader for multipart content in the request
                var multipartReader = Request.GetMultipartReader();
                var section = await multipartReader.ReadNextSectionAsync();

                // Multiple files/sections will not be saved. Only read the first one
                if (section == null)
                {
                    return BadRequest();
                }

                return await _airtableCsvImporter.ImportFromStream(section.Body, recipientId);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Airtable import failed.");
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [RequestSizeLimit(104857600)]
        [DisableFormModelBinding]
        [Route("nationlPost/recipient/{recipientId:Guid}", Name = nameof(ImportNationalPost))]
        public async Task<ActionResult<ImportResultDto>> ImportNationalPost(Guid recipientId)
        {

            EnableSynchronousIO();

            try
            {
                if (!Request.HasFormContentType)
                {
                    return new UnsupportedMediaTypeResult();
                }

                // GetById a reader for multipart content in the request
                var multipartReader = Request.GetMultipartReader();
                var section = await multipartReader.ReadNextSectionAsync();

                // Multiple files/sections will not be saved. Only read the first one
                if (section == null)
                {
                    return BadRequest();
                }

                return await _npCsvImporter.ImportFromStream(section.Body, recipientId);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "National Post import failed.");
                return BadRequest(ex);
            }
        }

        private void EnableSynchronousIO()
        {
            var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
            if (syncIOFeature != null)
            {
                syncIOFeature.AllowSynchronousIO = true;
            }
        }
#endif
    }
}