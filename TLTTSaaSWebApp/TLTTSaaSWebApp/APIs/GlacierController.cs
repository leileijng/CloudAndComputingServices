using Amazon.Glacier;
using Amazon.Glacier.Model;
using Amazon.Glacier.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TLTTSaaSWebApp.APIs
{
    public class GlacierController : ApiController
    {
        [Authorize(Roles = "Premium,Free")]
        [HttpPost]
        [Route("api/glacier/uploadArchive")]
        public IHttpActionResult uploadArchive(string filePath)
        {
            try
            {
                string vaultName = "cscca2";
                var path = @"C:\Glacier\" + filePath;
                var manager = new ArchiveTransferManager(Amazon.RegionEndpoint.USEast1);
                // Upload an archive.
                string archiveId = manager.Upload(vaultName, "upload archive test", path).ArchiveId;
                return Ok(archiveId);
            }
            catch (AmazonGlacierException e)
            {
                return BadRequest(e.ToString());
            }
            catch (Exception e) {
                return BadRequest(e.ToString());
            }
        }
    }
}
