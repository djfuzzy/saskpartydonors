using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using SaskPartyDonors.Extensions;
using SaskPartyDonors.Filters;
using SaskPartyDonors.Services.Importers;

namespace SaskPartyDonors.Controllers
{
  [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly SaskCsvImporter _saskCsvImporter;

        private readonly AirtableCsvImporter _airtableCsvImporter;

        public ImportController(SaskCsvImporter saskCsvImporter,
          AirtableCsvImporter airtableCsvImporter)
        {
            _saskCsvImporter = saskCsvImporter;
            _airtableCsvImporter = airtableCsvImporter;
        }

        [HttpPost]
        [RequestSizeLimit(52428800)]
        [DisableFormModelBinding]
        [Route("year/{year:int}", Name = nameof(ImportWithYear))]
        public async Task<ActionResult> ImportWithYear(int year)
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

                _saskCsvImporter.ImportFromStream(section.Body, year);

                return Ok();
            }
            catch (Exception e)
            {
                // TODO: (forsberd) Handle errors better
                throw;
            }
        }

        [HttpPost]
        [RequestSizeLimit(52428800)]
        [DisableFormModelBinding]
        [Route("recipient/{recipient}", Name = nameof(ImportWithRecipient))]
        public async Task<ActionResult> ImportWithRecipient(string recipient)
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

                _airtableCsvImporter.ImportFromStream(section.Body, recipient);

                return Ok();
            }
            catch (Exception e)
            {
                // TODO: (forsberd) Handle errors better
                throw;
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
    }
}