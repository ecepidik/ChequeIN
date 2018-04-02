/**
 * This represents an already submitted cheque req. We get objects of this shape from the server.
 *
 * @todo: Update this to match the backend model
 */
export interface SubmittedChequeReq {
  preTax: number;
  GST: number;
  PST: number;
  HST: number;
  description: string;
  onlinePurchase: boolean;
  payableAddressee: string;
  approver: string;
  account: Account;
  freeFood: boolean;
  mailCheque: boolean;
  mailingAddress: string;
}
