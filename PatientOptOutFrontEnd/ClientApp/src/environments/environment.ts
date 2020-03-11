// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  apiUrl: 'https://optoutapidev.plymouth.nhs.uk',
  trustDisclaimer:
  `
  This app has been developed to ensure University Hospital Plymouth NHS Trust complies with the NHS National Data Opt Out Programme.
  <br/><br/>
  National Data Opt Out applies to some national audits and research. This means that the Trust must not submit data for patients who have opted out.
  <br/><br/>
  The app allows staff responsible for entering data for these audits and research to check the patients’ opt out status.
  <br/><br/>
  For more information about the National Data Opt Out Programme please see the national guidance from NHS Digital:
  <a href="https://digital.nhs.uk/services/national-data-opt-out">https://digital.nhs.uk/services/national-data-opt-out</a> and our Trust Privacy Notice:
  <a href="https://www.plymouthhospitals.nhs.uk/your-personal-info">https://www.plymouthhospitals.nhs.uk/your-personal-info</a>
  `,
  initialDisclaimer:
  `
  The central register of nationally opted out patients is held by NHS Digital.
  <br/><br/>
  University Hospitals Plymouth NHS Trust update the local data warehouse once a week with the nationally opted out patients. This app queries the local data warehouse.
  <br/><br/>
  This app should only be used for dataflows that are in scope of the National Opt Out. Access is audited.
  `,
  noAccess:
  `
  <p>To gain access, please contact the Information Governance team on 37284 for more information</p>
  <br/>
  <p>If you believe that you have the required access and are seeing this screen in error, please contact Service Desk on 37000</p>
  `
};
