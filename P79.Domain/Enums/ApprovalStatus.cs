using System;
using System.Collections.Generic;
using System.Text;

namespace P79.Domain.Enums
{
    public enum ApprovalStatus
    {
        Submitted,
        AssignedToAO,
        UpdateDocument,
        DocumentUpdated,
        ApprovedAO,
        Rejected,
        Approved,
        RejectedApprover,
        Completed
    }
}
