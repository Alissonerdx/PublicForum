using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum.Domain.Validations
{
    public class CreateTopicValidation : TopicValidation
    {
        public CreateTopicValidation()
        {
            ValidateTitle();
            ValidateDescription();
            ValidateCreateDate();
        }
    }
}
