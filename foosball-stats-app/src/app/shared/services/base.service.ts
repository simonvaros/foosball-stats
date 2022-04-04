import { HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({ providedIn: "root" })
export class BaseService {
  protected httpOptions = {
    headers: new HttpHeaders({
      "Access-Control-Allow-Origin": "*",
      "Content-Type": `application/json`,
      "Cache-Control": "no-cache",
      Accept: "*/*",
    }),
  };

  constructor() {}
}
