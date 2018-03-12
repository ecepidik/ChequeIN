/**
 * This class represents an already submitted cheque req. We get objects of this class from the server.
 */
export class SubmittedChequeReq {
  preTax: number = 0;
  GST: number = 0;
  PST: number = 0;
  HST: number = 0;
  description: string = '';
  onlinePurchase: boolean = false;
  payableAddressee: string = '';
  approver: string = '';
  account: Account | undefined;
  freeFood: boolean = false;
  public mailCheque: boolean = false;
  public mailingAddress: string = '';
}
