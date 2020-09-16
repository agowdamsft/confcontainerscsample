# Confidential Containers Samples

The term “confidential containers” refers to docker application (new or existing) containers packaged with additional components if necessary to run on the hardware that provides strong protections of Confidential Computing as listed above and meets the below list of criteria to improve the overall security posture of the container application and the data-in-use.

Confidential containers is about taking an existing docker container application and running it on a hardware based Trusted Execution Environment (enclave). This packaging and execution protects the data-in-use with added benefits of container IP protection through container encryption and secure mounting of containers in the host to isolation execution.

[Read more here](http://aka.ms/confidentialcomputing)

## Samples Collection Index

This repo is organized by folders that states the sample name followed by the enablers of confidential containers. A typical folder name follows this standard < samplename >-< enabername > :

* [Confidential HealthCare Demo With Scone, Confidential Inferencing & Azure Attestation](confidential-healthcare-scone-confinf/README.md) 
* Feature 2
* ...

## Getting Started

### Prerequisites

This implementation assumed the samples would be deployed into Azure Kubernetes Service (AKS) with Confidential Computing Nodes.


- OS
- Library version
- ...

### Installation

(ideally very short)

- npm install [package name]
- mvn install
- ...

### Quickstart
(Add steps to get up and running quickly)

1. git clone [repository clone url]
2. cd [respository name]
3. ...


## Demo

A demo app is included to show how to use the project.

To run the demo, follow these steps:

(Add steps to start up the demo)

1.
2.
3.

## Resources

(Any additional resources or related projects)

- Link to supporting information
- Link to similar sample
- ...
