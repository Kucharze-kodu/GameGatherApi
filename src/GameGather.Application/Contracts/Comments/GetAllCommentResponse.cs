using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGather.Application.Contracts.Comments
{
    public record GetAllCommentResponse(
        int Id,
        string Text,
        DateTime DateComment
        );
}
