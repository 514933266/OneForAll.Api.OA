using Microsoft.AspNetCore.Http;
using OA.Host.Models;
using OA.Public.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OA.Host
{
    public class TenantProvider : ITenantProvider
    {
        private IHttpContextAccessor _context;

        public TenantProvider(IHttpContextAccessor context)
        {
            _context = context;
        }

        public Guid GetTenantId()
        {
            var tenantId = _context.HttpContext?.User.Claims.FirstOrDefault(e => e.Type == UserClaimType.TENANT_ID);
            if (tenantId != null)
            {
                return new Guid(tenantId.Value);
            }
            else
            {
                return Guid.Empty; ;
            }
        }
    }
}
