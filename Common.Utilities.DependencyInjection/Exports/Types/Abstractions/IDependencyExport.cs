/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using System.Collections.Generic;

namespace Common.Utilities.DependencyInjection.Exports.Types.Abstractions
{
		public interface IDependencyExport
		{
				IEnumerable<IServiceExport> GetServiceExports();
				IEnumerable<ISettingsExport> GetSettingsExports();
		}
}
