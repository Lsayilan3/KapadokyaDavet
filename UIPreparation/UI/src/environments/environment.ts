// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

export const environment = {
  production: false,
  getApiUrl: "https://api.kapadokyadavet.com/api",
  getDropDownSetting: {
    singleSelection: false,
    idField: 'id',
    textField: 'label',
    selectAllText: 'Select All',
    unSelectAllText: 'UnSelect All',
    itemsShowLimit: 3,
    allowSearchFilter: true
  },
  getDatatableSettings:  {
    pagingType: 'full_numbers',
    pageLength: 2
  }

};
// https://localhost:44375/WebAPI/api
// https://api.kapadokyadavet.com/api