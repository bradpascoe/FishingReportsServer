using System;
using System.Collections.Generic;
using System.ServiceModel;

using FishingReports.Model;

namespace FishingReportsServices
{
	[ServiceContract]
	public interface ILocationDataService
	{
		#region ILocationDataService Members

		[OperationContract]
		IEnumerable<AccessEntity> GetAccesses(Guid locationId);

		[OperationContract]
		IEnumerable<LocationEntity> GetAllLocations();

		[OperationContract]
		IEnumerable<LocationEntity> GetLocations(Guid stateId);

		[OperationContract]
		IEnumerable<StateEntity> GetStates();

		[OperationContract]
		void SaveAccess(Guid locationId, string newAccess);

		[OperationContract]
		void SaveLocation(Guid stateId, string newLocation);

		[OperationContract]
		void SaveState(string newState);

		[OperationContract]
		IDictionary<string, string> ValidateNewAccess(AccessEntity access);

		[OperationContract]
		IDictionary<string, string> ValidateNewLocation(LocationEntity location);

		[OperationContract]
		IDictionary<string, string> ValidateNewState(StateEntity state);

		#endregion ILocationDataService Members

	}
}
