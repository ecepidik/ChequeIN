export class ChequeReq {
  public preTax: number = 0;
  public GST: number = 0;
  public PST: number = 0;
  public HST: number = 0;
  public description: string = '';
  public onlinePurchase: boolean = false;
  public payableAddressee: string = '';
  public approver: string = '';
  public account: Account | undefined;
  public freeFood: boolean;
  public mailCheque: boolean = false;
  public mailingAddress: string = '';
}
