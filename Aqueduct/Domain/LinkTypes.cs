
using System;

namespace Aqueduct.Domain
{
    [Serializable]
    public enum LinkTypes
	{
		None,
		Internal,
		External,
		Media,
		MailTo,
		JavaScript,
		Anchor
	}
}
