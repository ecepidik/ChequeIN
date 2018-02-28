export class ChequeReq {
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
    mailCheque: boolean = false;
    mailingAddress: string = '';
}

