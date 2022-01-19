using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum.Domain.Validations.Topic
{
    public class DeleteTopicValidation : TopicValidation
    {
        public DeleteTopicValidation()
        {
            ValidateId();
        }
    }
}
