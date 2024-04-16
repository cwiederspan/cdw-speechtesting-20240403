# cdw-speechtesting-20240403
A test against Microsoft AI speech services.

```bash

# Tenant ID: 16b3c013-d300-468d-ac64-7eda0820b6d3

# App ID: f5d41d9d-ac20-4941-a672-76ce093dae72

# SP Obj ID: 092011ee-5513-491b-acce-277918383564

az ad app create --display-name cdw-speechtesting-20240403-app

az ad app credential reset --id f5d41d9d-ac20-4941-a672-76ce093dae72 --display-name myappcredential --end-date '2024-12-31'

az ad sp create --id f5d41d9d-ac20-4941-a672-76ce093dae72

az role assignment create --assignee 092011ee-5513-491b-acce-277918383564 --role 'Cognitive Services User' --scope /subscriptions/30c417b6-b3c1-4b62-94c9-0d3a80a182e9/resourceGroups/cdw-speechtest-20240403/providers/Microsoft.CognitiveServices/accounts/cdw-speechtest-20240403-stt

```