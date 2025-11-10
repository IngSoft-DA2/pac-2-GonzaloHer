import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class AccessCounterService {
  private routeAccessCount: Map<string, number> = new Map();

  increment(route: string): void {
    const current = this.routeAccessCount.get(route) || 0;
    this.routeAccessCount.set(route, current + 1);
  }

  getCount(route: string): number {
    return this.routeAccessCount.get(route) || 0;
  }
}
