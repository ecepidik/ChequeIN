/**
 * This class represents a cheque Req Submission. This is what we send to the server in a post request.
 */
export class ChequeReqSubmission {
  public preTax: number = 0;
  public GST: number = 0;
  public PST: number = 0;
  public HST: number = 0;
  public description: string = '';
  public onlinePurchase: boolean = false;
  public payableAddressee: string = '';
  public approver: string = '';
  public account: Account | undefined;
  public freeFood: boolean = false;
  public mailCheque: boolean = false;
  public mailingAddress: string = '';
  public files: FileList | File;
  public fileDescriptions: { [key: string]: string } = {};
}
