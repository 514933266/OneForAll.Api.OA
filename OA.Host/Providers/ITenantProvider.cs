using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Host
{
    public interface ITenantProvider
    {
        Guid GetTenantId();
    }
}
