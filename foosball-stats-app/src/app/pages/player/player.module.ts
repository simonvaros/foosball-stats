import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlayerRoutingModule } from './player-routing.module';

import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatExpansionModule } from '@angular/material/expansion';
import { PlayerDetailComponent } from './player-detail/player-detail.component';
import { PlayersListComponent } from './players-list/players-list.component';
import { DxToolbarModule } from 'devextreme-angular/ui/toolbar';

@NgModule({
  declarations: [PlayerDetailComponent, PlayersListComponent],
  imports: [
    CommonModule,
    PlayerRoutingModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatExpansionModule,
    DxToolbarModule
  ],
})
export class PlayerModule {}
