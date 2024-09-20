import { InjectionToken } from "@angular/core";
import { UrlConfig } from "../interface/url-config";

export const URL_CONFIG = new InjectionToken<UrlConfig>('config');

export const urlConfig: UrlConfig = {
    baseApiUrl: 'http://localhost:5237'
}