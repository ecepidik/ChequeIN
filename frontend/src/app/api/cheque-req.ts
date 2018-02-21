export class ChequeReq {
  public description: string = '';
  public onlinePurchase: boolean = false;
  public payableAddressee: string = '';
  public approver: string = '';
  public account: Account | undefined;
  public freeFood: boolean = false;
  public mailCheque: boolean = false;
  public mailingAddress: string = '';
}
