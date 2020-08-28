# Confidential Container Samples
This repo lists the samples aggregated from Microsoft Confidential ISV or Open Source Project that support of unmodified container application written with higher programming languages. Project focuses on Azure Kubernetes Service (AKS) based deployed leveraging confidential computing nodes. 

## Sample 1 : multi-party trust through SGX container applications
### Scenario
2 parties - Lamna Hospital and Contoso software provider

Lamna doesn't want to share with Contoso its patients' PII or sensitive data for several reasons e.g. compliance, confidentiality, regulation, etc.

Contoso would like to provide Lamna software solutions but is concerned about sharing proprietary intellectual property e.g. models, and algorithms.

Step 1: A new neurology patient is being admitted to the hospital and personal details are being collected and transmitted to confidential container-based web API.

Step 2: Brain MRI sections are ordered and collected. This is run through a pretrained [public ML scoring model](https://github.com/mateuszbuda/brain-segmentation-pytorch)  that’s uses features from the MRI and associates to subtypes of cancer.

### Architecture
